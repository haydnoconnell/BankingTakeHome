using BankingTakeHome.Domain;
namespace BankingTakeHome.Application;

public class AccountService(IAccountRepository repository) : IAccountService
{
    private static string NewId()
    {
        return Guid.NewGuid().ToString();
    }

    public async Task<string> CreateAccountAsync(string ownerName, decimal initialDeposit)
    {
        var accountId = NewId();
        var account = new Account(accountId, ownerName, initialDeposit);
        await repository.CreateAsync(account);
        return accountId;
    }

    public async Task DepositAsync(string accountId, decimal amount)
    {
        var account = await repository.GetAsync(accountId);
        
        if(account is null)
        {
            throw new InvalidOperationException($"Account with id {accountId} not found");
        }
        
        account.Deposit(amount);
        
        await repository.UpdateAsync(account);
    }
    
    public async Task WithdrawAsync(string accountId, decimal amount)
    {
        var account = await repository.GetAsync(accountId);
        
        if(account is null)
        {
            throw new InvalidOperationException($"Account with id {accountId} not found");
        }
        
        account.Withdraw(amount);
        
        await repository.UpdateAsync(account);
    }

    public async Task TransferAsync(string fromAccountId, string toAccountId, decimal amount)
    {
        if (fromAccountId == toAccountId)
        {
            throw new ArgumentException("Unable to transfer to same account");
        }

        var fromAccount = await repository.GetAsync(fromAccountId);
        if (fromAccount is null)
        {
            throw new InvalidOperationException($"Unable to complete transfer. Sender Account with id {fromAccountId} not found");
        }

        var toAccount = await repository.GetAsync(toAccountId);
        if (toAccount is null)
        {
            throw new InvalidOperationException($"Unable to complete transfer. Receiver Account with id {toAccountId} not found");
        }

        fromAccount.Transfer(fromAccount, toAccount, amount);

        await repository.UpdateAsync(fromAccount);
        await repository.UpdateAsync(toAccount);
    }

    public async Task<decimal> GetBalanceAsync(string accountId)
    {
        var account = await repository.GetAsync(accountId);
        if (account is null)
        {
            throw new InvalidOperationException($"Account with id {accountId} not found");
        }

        return account.Balance;
    }
}