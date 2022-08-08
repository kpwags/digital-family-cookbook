namespace DigitalFamilyCookbook.Data.Interfaces;

public interface IRefreshTokenRepository
{
    IEnumerable<RefreshToken> GetUserRefreshTokens(string token);

    RefreshToken? GetRefreshTokenByToken(string token);

    Task RevokeToken(RefreshToken token, string ipAddress, string? reason = null, string? replacedByToken = null);

    Task RemoveOldRefreshTokens(IEnumerable<RefreshToken> tokens, int ttl);

    Task Add(RefreshToken refreshToken);
}
