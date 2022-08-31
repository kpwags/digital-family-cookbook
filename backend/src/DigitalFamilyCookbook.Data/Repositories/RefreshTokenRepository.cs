using Microsoft.EntityFrameworkCore;

namespace DigitalFamilyCookbook.Data.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ApplicationDbContext _db;

    public RefreshTokenRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public IEnumerable<RefreshToken> GetUserRefreshTokens(string token)
    {
        return _db.RefreshTokens
            .Where(rt => rt.Token == token)
            .Include(rt => rt.UserAccount)
            .Select(rt => RefreshToken.FromDto(rt));
    }

    public RefreshToken? GetRefreshTokenByToken(string token)
    {
        var refreshToken = _db.RefreshTokens.Where(rt => rt.Token == token).Include(rt => rt.UserAccount).FirstOrDefault();

        return refreshToken is not null ? RefreshToken.FromDto(refreshToken) : null;
    }

    public async Task RevokeToken(RefreshToken token, string ipAddress, string? reason = null, string? replacedByToken = null)
    {
        var refreshToken = _db.RefreshTokens.FirstOrDefault(rt => rt.Token == token.Token);

        if (refreshToken is null)
        {
            return;
        }

        refreshToken.Revoked = DateTime.UtcNow;
        refreshToken.RevokedByIp = ipAddress;
        refreshToken.ReasonRevoked = reason;
        refreshToken.ReplacedByToken = replacedByToken;

        await _db.SaveChangesAsync();
    }

    public async Task RemoveOldRefreshTokens(IEnumerable<RefreshToken> tokens, int ttl)
    {
        var expiredTokenIds = tokens
                .Where(t => !t.IsActive && t.DateCreated.AddDays(ttl) <= DateTime.UtcNow)
                .Select(t => t.Id);

        var refreshTokens = _db.RefreshTokens.Where(rt => expiredTokenIds.Contains(rt.RefreshTokenId));

        _db.RefreshTokens.RemoveRange(refreshTokens);

        await _db.SaveChangesAsync();
    }

    public async Task Add(RefreshToken refreshToken)
    {
        var dto = new RefreshTokenDto
        {
            Token = refreshToken.Token,
            Expires = refreshToken.Expires,
            CreatedByIp = refreshToken.CreatedByIp,
            Revoked = refreshToken.Revoked,
            RevokedByIp = refreshToken.RevokedByIp,
            ReplacedByToken = refreshToken.ReplacedByToken,
            ReasonRevoked = refreshToken.ReasonRevoked,
            UserAccount = _db.UserAccounts.First(u => u.Id == refreshToken.UserAccount.Id),
        };

        _db.RefreshTokens.Add(dto);

        await _db.SaveChangesAsync();
    }
}