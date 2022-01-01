namespace DigitalFamilyCookbook.Services;

public interface IAuthService
{
    Task<bool> SignInUser();

    Task<bool> SignOutUser();
}
