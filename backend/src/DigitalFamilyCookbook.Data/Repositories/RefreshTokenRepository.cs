namespace DigitalFamilyCookbook.Data.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ApplicationDbContext _db;

    public RefreshTokenRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<RefreshTokenDto> Add(RefreshTokenDto refreshToken)
    {
        var token = await _db.RefreshTokens.AddAsync(refreshToken);
        await _db.SaveChangesAsync();

        return token.Entity;
    }
}