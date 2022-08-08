using System.Security.Claims;

namespace DigitalFamilyCookbook.Core.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(List<Claim> claims);

    (string? userId, string? error) ValidateAccessToken(string token);

    Task<RefreshToken> GenerateRefreshToken(string ipAddress, UserAccount user);
}