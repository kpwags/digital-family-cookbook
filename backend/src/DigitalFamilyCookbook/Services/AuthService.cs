using DigitalFamilyCookbook.Configuration;
using DigitalFamilyCookbook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace DigitalFamilyCookbook.Services;

public class AuthService : IAuthService
{
    private readonly DigitalFamilyCookbookConfiguration _configuration;
    private readonly UserManager<UserAccount> _userManager;
    private readonly SignInManager<UserAccount> _signInManager;

    public AuthService(DigitalFamilyCookbookConfiguration configuration,
        UserManager<UserAccount> userManager,
        SignInManager<UserAccount> signInManager)
    {
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public AuthToken GetAuthToken(string userId)
    {
        var expiration = DateTime.UtcNow.AddHours(_configuration.Auth.JwtLifespan);
        var token = GenerateJwtToken(userId, expiration);

        return new AuthToken
        {
            Id = userId,
            Token = token,
            TokenExpirationTime = ((DateTimeOffset)expiration).ToUnixTimeSeconds(),
        };
    }

    public async Task<(bool IsValid, string UserId)> VerifyCredentials(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user != null)
        {
            var passwordCheck = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            return (passwordCheck.Succeeded, user.UserId);
        }

        return (false, string.Empty);
    }

    private string GenerateJwtToken(string userId, DateTime expirationDate)
    {
        var claims = new[] {
            new Claim("id", userId),
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var key = System.Text.Encoding.ASCII.GetBytes(_configuration.Auth.JwtSecret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expirationDate,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}