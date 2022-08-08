using Microsoft.AspNetCore.Identity;

namespace DigitalFamilyCookbook.Data.Repositories;

public class UserAccountRepository : IUserAccountRepository
{
    private readonly UserManager<UserAccountDto> _userManager;
    private readonly RoleManager<RoleTypeDto> _roleManager;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public UserAccountRepository(
        UserManager<UserAccountDto> userManager,
        RoleManager<RoleTypeDto> roleManager,
        IRefreshTokenRepository refreshTokenRespository)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _refreshTokenRepository = refreshTokenRespository;
    }

    public async Task<UserAccount> GetUserAccountById(string userAccountId, bool includeRoles = false)
    {
        var user = await _userManager.FindByIdAsync(userAccountId);

        if (user is null)
        {
            return UserAccount.None();
        }

        if (includeRoles)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = new List<RoleTypeDto>();

            foreach (var userRole in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(userRole);

                if (role != null)
                {
                    roles.Add(role);
                }
            }

            user.RoleTypes = roles;
        }

        return UserAccount.FromDto(user);
    }

    public async Task<UserAccount?> GetUserAccountByIdOrDefault(string userAccountId)
    {
        var user = await _userManager.FindByIdAsync(userAccountId);

        return user is not null ? UserAccount.FromDto(user) : null;
    }

    public async Task<UserAccount?> GetUserAccountByToken(string token)
    {
        var userTokens = _refreshTokenRepository.GetUserRefreshTokens(token);

        if (!userTokens.Any())
        {
            return null;
        }

        var t = userTokens.First();
        var u = t.UserAccount;

        var user = await _userManager.FindByIdAsync(userTokens.First().UserAccount.Id);

        return user is not null ? UserAccount.FromDto(user) : null;
    }

    public async Task<IEnumerable<UserAccount>> GetAllUserAccounts()
    {
        var users = await Task.FromResult(_userManager.Users.ToList());

        return users.Select(u => UserAccount.FromDto(u));
    }

    public async Task DeleteUserAccount(string userAccountId)
    {
        var user = await _userManager.FindByIdAsync(userAccountId);

        if (user is null)
        {
            throw new Exception("User account not found");
        }

        await _userManager.DeleteAsync(user);
    }
}