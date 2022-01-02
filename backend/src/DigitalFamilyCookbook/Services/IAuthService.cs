using DigitalFamilyCookbook.Models;

namespace DigitalFamilyCookbook.Services;

public interface IAuthService
{
    AuthToken GetAuthToken(string userId);

    Task<(bool IsValid, string UserId)> VerifyCredentials(string email, string password);
}
