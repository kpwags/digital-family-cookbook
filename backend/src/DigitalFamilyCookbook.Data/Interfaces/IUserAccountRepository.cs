namespace DigitalFamilyCookbook.Data.Interfaces;

public interface IUserAccountRepository
{
    Task<UserAccount> GetUserAccountById(string userAccountId, bool includeRoles = false);

    Task<UserAccount?> GetUserAccountByIdOrDefault(string userAccountId);

    Task<UserAccount?> GetUserAccountByToken(string token);

    Task<IEnumerable<UserAccount>> GetAllUserAccounts();

    Task DeleteUserAccount(string userAccountId);
}