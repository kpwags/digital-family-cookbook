namespace DigitalFamilyCookbook.Data.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshTokenDto> Add(RefreshTokenDto refreshToken);
}