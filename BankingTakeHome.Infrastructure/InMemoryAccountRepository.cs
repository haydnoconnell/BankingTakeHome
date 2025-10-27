using BankingTakeHome.Domain;

namespace BankingTakeHome.Infrastructure;

public class InMemoryAccountRepository : IAccountRepository
{
    private readonly Dictionary<string, Account> _accounts = new();

    public Task<Account> CreateAsync(Account account)
    {
        if (_accounts.ContainsKey(account.Id))
        {
            throw new InvalidOperationException($"Account {account.Id} already exists");
        }
        
        //Account is stored in memory for testing
        _accounts[account.Id] = account;
        
        return Task.FromResult(account);
    }
    
    //Account is already stored in memory 
    public Task UpdateAsync(Account account)
    {
        return Task.CompletedTask; 
    }

    public Task<Account> GetAsync(string id)
    {
        if (!_accounts.TryGetValue(id, out var account))
        {
            throw new InvalidOperationException($"Account {id} not found");
        }
        
        return Task.FromResult(account);
    }
}