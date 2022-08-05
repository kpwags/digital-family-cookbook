using System.Security.Claims;

namespace DigitalFamilyCookbook.Core.Interfaces;

public interface ITokenService
{
    string GenerateJwtToken(List<Claim> claims);

    (string? userId, string? error) ValidateJwtToken(string token);

    Task<RefreshToken> GenerateRefreshToken(string ipAddress, UserAccount user);
}