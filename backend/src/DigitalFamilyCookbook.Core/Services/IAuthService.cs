using DigitalFamilyCookbook.Data.Models;

namespace DigitalFamilyCookbook.Core.Services;

public interface IAuthService
{
    Task<AuthResult> LoginUser(string email, string password);

    Task<AuthResult> RegisterUser(string email, string password, string name);
}