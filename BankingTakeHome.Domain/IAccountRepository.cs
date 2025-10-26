namespace BankingTakeHome.Domain;

public interface IAccountRepository
{
    Task<Account> CreateAsync(Account account);
    Task<Account> GetAsync(string id);
    Task UpdateAsync(Account account);
}