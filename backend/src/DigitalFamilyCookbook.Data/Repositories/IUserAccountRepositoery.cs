namespace DigitalFamilyCookbook.Data.Repositories;

public interface IUserAccountRepository
{
    Task<UserAccount> GetUserAccountById(string userAccountId);

    Task<IEnumerable<UserAccount>> GetAllUserAccounts();

    Task DeleteUserAccount(string userAccountId);
}