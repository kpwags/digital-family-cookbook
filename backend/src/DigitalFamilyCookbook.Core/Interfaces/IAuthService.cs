namespace DigitalFamilyCookbook.Core.Interfaces;

public interface IAuthService
{
    Task<AuthResult> LoginUser(string email, string password, string ip);

    Task<AuthResult> RegisterUser(string email, string password, string name, string ip);

    Task<AuthResult> RefreshToken(string token, string ip);

    Task RevokeToken(string token, string ip);
}