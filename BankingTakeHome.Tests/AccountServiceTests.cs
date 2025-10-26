using BankingTakeHome.Application;
using BankingTakeHome.Infrastructure;
using Xunit;

namespace BankingTakeHome.Tests;

public class AccountServiceTests
{
    private readonly IAccountService _accountService;

    public AccountServiceTests()
    {
        _accountService = new AccountService(new InMemoryAccountRepository());
    }
    
    [Fact]
    public async Task Can_Create_Deposit_Withdraw_Account()
    {
        var testAccount = await _accountService.CreateAccountAsync("TestUser", 100m);
        await _accountService.DepositAsync(testAccount,100m);
        await _accountService.WithdrawAsync(testAccount, 50m);
        var balance = await _accountService.GetBalanceAsync(testAccount);
        
        Assert.Equal(150m, balance);
    }

    [Fact]
    public async Task Withdraw_InsufficientFunds_Should_Throw()
    {
        var id = await _accountService.CreateAccountAsync("TestUser", 100m);
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => _accountService.WithdrawAsync(id, 101m));
    }

    [Fact]
    public async Task Transfer_Works_NoOverdraft()
    {
        var fromTestAccount  = await _accountService.CreateAccountAsync("FromTestUser", 100m);
        var toTestAccount  = await _accountService.CreateAccountAsync("toTestUser", 100m);
        
        await _accountService.TransferAsync(fromTestAccount, toTestAccount, 50m);
        
        var fromAccountNewBalance = await _accountService.GetBalanceAsync(fromTestAccount);
        var toAccountNewBalance = await _accountService.GetBalanceAsync(toTestAccount);
        
        Assert.Equal(50m, fromAccountNewBalance);
        Assert.Equal(150m, toAccountNewBalance);
        await Assert.ThrowsAsync<InvalidOperationException>(() => _accountService.TransferAsync(fromTestAccount, toTestAccount, 101m));
    }

    [Fact]
    public async Task Self_Transfer_Should_Throw()
    {
        var testAccount = await _accountService.CreateAccountAsync("TestUser", 100m);
        await Assert.ThrowsAsync<ArgumentException>(() => _accountService.TransferAsync(testAccount, testAccount, 50m));
        
    }
}