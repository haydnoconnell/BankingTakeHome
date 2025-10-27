using BankingTakeHome.Application;
using BankingTakeHome.Infrastructure;

namespace BankingTakeHome.Demo;

public class Program
{
    public static async Task Main(string [] args)
    {
        var repository = new InMemoryAccountRepository();
        var accountService = new AccountService(repository);

        var accountA  = await accountService.CreateAccountAsync("FromTestUser", 100m);
        var accountB  = await accountService.CreateAccountAsync("FromTestUser", 100m);

        await accountService.DepositAsync(accountA, 100m);
        await accountService.WithdrawAsync(accountB, 50m);
        await accountService.TransferAsync(accountA, accountB, 150m);

        var balanceA = await accountService.GetBalanceAsync(accountA);
        var balanceB = await accountService.GetBalanceAsync(accountB);

        Console.WriteLine($"Account A Balance: {balanceA}");
        Console.WriteLine($"Account B Balance: {balanceB}");
    }
}

