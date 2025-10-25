using BankingTakeHome.Domain;

namespace BankingTakeHome.Application;

public interface IAccountService
{
    Task<string> CreateAccountAsync(string ownerName, decimal initialDeposit);
    Task DepositAsync(string accountId, decimal amount);
    Task WithdrawAsync(string accountId, decimal amount);
    Task TransferAsync(string fromAccountId, string toAccountId, decimal amount);
    
}