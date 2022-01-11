namespace DigitalFamilyCookbook.Data.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> Add(RefreshToken refreshToken);
}