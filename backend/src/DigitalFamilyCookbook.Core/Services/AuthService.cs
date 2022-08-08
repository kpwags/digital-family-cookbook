using DigitalFamilyCookbook.Core.Configuration;
using DigitalFamilyCookbook.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DigitalFamilyCookbook.Core.Services;

public class AuthService : IAuthService
{
    private readonly DigitalFamilyCookbookConfiguration _configuration;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly UserManager<UserAccountDto> _userManager;
    private readonly SignInManager<UserAccountDto> _signInManager;
    private readonly RoleManager<RoleTypeDto> _roleManager;
    private readonly IUserAccountRepository _userAccountRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenService _tokenService;

    public AuthService(DigitalFamilyCookbookConfiguration configuration,
        UserManager<UserAccountDto> userManager,
        SignInManager<UserAccountDto> signInManager,
        RoleManager<RoleTypeDto> roleManager,
        TokenValidationParameters tokenValidationParameters,
        IUserAccountRepository userAccountRepository,
        IRefreshTokenRepository refreshTokenRespository,
        ITokenService tokenService)
    {
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _tokenValidationParameters = tokenValidationParameters;
        _userAccountRepository = userAccountRepository;
        _refreshTokenRepository = refreshTokenRespository;
        _tokenService = tokenService;
    }

    public async Task<AuthResult> LoginUser(string email, string password, string ip)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user != null)
        {
            var passwordCheck = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (passwordCheck.Succeeded)
            {
                var claims = await BuildClaimsForUser(user);
                var token = _tokenService.GenerateJwtToken(claims);
                var userAccount = UserAccount.FromDto(user);

                var refreshToken = await _tokenService.GenerateRefreshToken(ip, userAccount);

                return new AuthResult
                {
                    IsSuccessful = true,
                    RefreshToken = refreshToken.Token,
                    AccessToken = token,
                };
            }
        }

        return new AuthResult
        {
            IsSuccessful = false,
            Error = "Invalid email or password",
        };
    }

    public async Task<AuthResult> RegisterUser(string email, string password, string name, string ip)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user != null)
        {
            return new AuthResult
            {
                IsSuccessful = false,
                Error = $"{email} already exists",
            };
        }

        var newUser = new UserAccountDto
        {
            Email = email,
            Name = name,
            UserName = email,
            UserId = Guid.NewGuid().ToString(),
        };

        var isCreated = await _userManager.CreateAsync(newUser, password);

        if (isCreated.Succeeded)
        {
            var createdUser = await _userManager.FindByEmailAsync(email);

            await _userManager.AddToRoleAsync(createdUser, "User");

            var claims = await BuildClaimsForUser(createdUser);

            var refreshToken = await _tokenService.GenerateRefreshToken(ip, UserAccount.FromDto(createdUser));
            var accessToken = _tokenService.GenerateJwtToken(claims);

            return new AuthResult
            {
                IsSuccessful = true,
                RefreshToken = refreshToken.Token,
                AccessToken = accessToken,
            };
        }

        return new AuthResult
        {
            IsSuccessful = false,
            Error = "Unable to create user account",
        };
    }

    public async Task<AuthResult> RefreshToken(string token, string ip)
    {
        var user = await _userAccountRepository.GetUserAccountByToken(token);
        var refreshToken = _refreshTokenRepository.GetRefreshTokenByToken(token);

        if (user is null || refreshToken is null)
        {
            return new AuthResult
            {
                IsSuccessful = false,
                Error = "Invalid token",
            };
        }

        if (refreshToken.IsRevoked)
        {
            // revoke all descendant tokens in case this token has been compromised
            await RevokeDescendantRefreshTokens(refreshToken, ip, $"Attempted reuse of revoked ancestor token: {token}");
        }

        if (!refreshToken.IsActive)
        {
            return new AuthResult
            {
                IsSuccessful = false,
                Error = "Invalid token",
            };
        }

        // replace old refresh token with a new one (rotate token)
        var newRefreshToken = await RotateRefreshToken(refreshToken, user, ip);

        var userRefreshTokens = _refreshTokenRepository.GetUserRefreshTokens(refreshToken.Token).ToList();
        // userRefreshTokens.Add(newRefreshToken);

        // remove old refresh tokens from user
        await _refreshTokenRepository.RemoveOldRefreshTokens(userRefreshTokens, _configuration.Auth.JwtLifespan);

        // generate new jwt
        var userDto = await _userManager.FindByIdAsync(user.Id);
        var claims = await BuildClaimsForUser(userDto);
        var jwtToken = _tokenService.GenerateJwtToken(claims);

        return new AuthResult
        {
            IsSuccessful = true,
            AccessToken = jwtToken,
            RefreshToken = newRefreshToken.Token,
        };
    }

    public async Task RevokeToken(string token, string ip)
    {
        var user = await _userAccountRepository.GetUserAccountByToken(token);

        if (user is null)
        {
            throw new Exception("Invalid token");
        }

        var refreshToken = _refreshTokenRepository.GetRefreshTokenByToken(token);

        if (refreshToken is null || !refreshToken.IsActive)
            throw new Exception("Invalid token");

        // revoke token and save
        await _refreshTokenRepository.RevokeToken(refreshToken, ip, "Revoked without replacement");
    }

    private async Task RevokeDescendantRefreshTokens(RefreshToken token, string ipAddress, string reason)
    {
        // recursively traverse the refresh token chain and ensure all descendants are revoked
        if (!string.IsNullOrEmpty(token.ReplacedByToken))
        {
            var childToken = _refreshTokenRepository.GetRefreshTokenByToken(token.ReplacedByToken);

            if (childToken is null)
            {
                return;
            }

            if (childToken.IsActive)
            {
                await _refreshTokenRepository.RevokeToken(childToken, ipAddress, reason);
            }
            else
            {
                await RevokeDescendantRefreshTokens(childToken, ipAddress, reason);
            }
        }
    }

    private async Task<RefreshToken> RotateRefreshToken(RefreshToken refreshToken, UserAccount user, string ipAddress)
    {
        var newRefreshToken = await _tokenService.GenerateRefreshToken(ipAddress, user);

        await _refreshTokenRepository.RevokeToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);

        return newRefreshToken;
    }

    private async Task<List<Claim>> BuildClaimsForUser(UserAccountDto user)
    {
        var claims = new List<Claim>();

        var userRoles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim("id", user.Id.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

        var userClaims = await _userManager.GetClaimsAsync(user);
        claims.AddRange(userClaims);

        foreach (var userRole in userRoles)
        {
            var role = await _roleManager.FindByNameAsync(userRole);

            if (role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));

                var roleClaims = await _roleManager.GetClaimsAsync(role);
                foreach (var roleClaim in roleClaims)
                {
                    claims.Add(roleClaim);
                }
            }
        }

        return claims;
    }
}