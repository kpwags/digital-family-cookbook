using Microsoft.AspNetCore.Identity;

namespace DigitalFamilyCookbook.Data.Repositories;

public class UserAccountRepository : IUserAccountRepository
{
    private readonly UserManager<UserAccountDto> _userManager;

    public UserAccountRepository(UserManager<UserAccountDto> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UserAccount> GetUserAccountById(string userAccountId)
    {
        var user = await _userManager.FindByIdAsync(userAccountId);

        if (user is null)
        {
            return UserAccount.None();
        }

        return UserAccount.FromDto(user);
    }
}