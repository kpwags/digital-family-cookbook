using DigitalFamilyCookbook.Core.Configuration;
using DigitalFamilyCookbook.Data.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DigitalFamilyCookbook.Core.Services;

public class TokenService : ITokenService
{
    private readonly DigitalFamilyCookbookConfiguration _configuration;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public TokenService(
        DigitalFamilyCookbookConfiguration configuration,
        TokenValidationParameters tokenValidationParameters,
        IRefreshTokenRepository refreshTokenRespository)
    {
        _configuration = configuration;
        _tokenValidationParameters = tokenValidationParameters;
        _refreshTokenRepository = refreshTokenRespository;
    }

    public string GenerateAccessToken(List<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.Auth.JwtSecret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public (string? userId, string? error) ValidateAccessToken(string token)
    {
        if (token == null)
        {
            return (null, "NO_TOKEN");
        }

        if (IsTokenExpired(token))
        {
            return (null, "EXPIRED");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.Auth.JwtSecret);

        try
        {
            tokenHandler.ValidateToken(token, _tokenValidationParameters, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

            // return user id from JWT token if validation successful
            return (userId, null);
        }
        catch
        {
            // return null if validation fails
            return (null, "INVALID");
        }
    }

    public async Task<RefreshToken> GenerateRefreshToken(string ipAddress, UserAccount user)
    {
        var refreshToken = new RefreshToken
        {
            Token = getUniqueToken(),
            Expires = DateTime.UtcNow.AddDays(7),
            DateCreated = DateTime.UtcNow,
            CreatedByIp = ipAddress,
            UserAccount = user,
        };

        await _refreshTokenRepository.Add(refreshToken);

        return refreshToken;

        string getUniqueToken()
        {
            // token is a cryptographically strong random sequence of values
            var token = Convert.ToBase64String(Encoding.ASCII.GetBytes(RandomString(64)));

            // ensure token is unique by checking against db
            var tokenIsUnique = _refreshTokenRepository.GetRefreshTokenByToken(token);

            if (tokenIsUnique is not null)
                return getUniqueToken();

            return token;
        }
    }

    private string RandomString(int length)
    {
        var random = new Random();
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        return new string(Enumerable.Repeat(chars, length)
            .Select(x => x[random.Next(x.Length)]).ToArray());
    }

    private bool IsTokenExpired(string token)
    {
        var jwtToken = new JwtSecurityToken(token);

        return (jwtToken.ValidFrom > DateTime.UtcNow) || (jwtToken.ValidTo < DateTime.UtcNow);
    }
}