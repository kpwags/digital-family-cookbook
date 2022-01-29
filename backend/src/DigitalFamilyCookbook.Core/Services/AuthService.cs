using DigitalFamilyCookbook.Core.Configuration;
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

    public AuthService(DigitalFamilyCookbookConfiguration configuration,
        UserManager<UserAccountDto> userManager,
        SignInManager<UserAccountDto> signInManager,
        RoleManager<RoleTypeDto> roleManager,
        TokenValidationParameters tokenValidationParameters)
    {
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _tokenValidationParameters = tokenValidationParameters;
    }

    public async Task<AuthResult> LoginUser(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user != null)
        {
            var passwordCheck = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (passwordCheck.Succeeded)
            {
                return await GenerateToken(user);
            }
        }

        return new AuthResult
        {
            IsSuccessful = false,
            Error = "Invalid email or password",
        };
    }

    public async Task<AuthResult> RegisterUser(string email, string password, string name)
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
            return await GenerateToken(newUser);
        }

        return new AuthResult
        {
            IsSuccessful = false,
            Error = "Unable to create user account",
        };
    }

    private async Task<AuthResult> GenerateToken(UserAccountDto user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var claims = await GetUserClaims(user);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(_configuration.Auth.JwtLifespan),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.Auth.JwtSecret)), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = jwtTokenHandler.WriteToken(token);

        return new AuthResult
        {
            Token = jwtToken,
            IsSuccessful = true,
        };
    }

    private async Task<List<Claim>> GetUserClaims(UserAccountDto user)
    {
        var claims = new List<Claim>
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

        // Getting the claims that we have assigned to the user
        var userClaims = await _userManager.GetClaimsAsync(user);
        claims.AddRange(userClaims);

        // Get the user role and add it to the claims
        var userRoles = await _userManager.GetRolesAsync(user);

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

    private string RandomString(int length)
    {
        var random = new Random();
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        return new string(Enumerable.Repeat(chars, length)
            .Select(x => x[random.Next(x.Length)]).ToArray());
    }
}