namespace DigitalFamilyCookbook.Data.Repositories;

public interface IUserAccountRepository
{
    Task<UserAccount> GetUserAccountById(string userAccountId);
}