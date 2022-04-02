namespace DigitalFamilyCookbook.Data.Interfaces;

public interface IUserAccountRepository
{
    Task<UserAccount> GetUserAccountById(string userAccountId);

    Task<UserAccount?> GetUserAccountByIdOrDefault(string userAccountId);

    Task<IEnumerable<UserAccount>> GetAllUserAccounts();

    Task DeleteUserAccount(string userAccountId);
}