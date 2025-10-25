using BankingTakeHome.Domain;
using Xunit;

namespace BankingTakeHome.Tests;

public class AccountTests
{
    [Fact]
    public void Deposit_Balance()
    {
        var account = new Account("1", "Test", 100m);
        account.Deposit(100m);
        Assert.Equal(200m, account.Balance);
    }
    [Fact]
    public void Deposit_Throws_AmountEqualsZero()
    {
        var account = new Account("1", "Test", 100m);
        Assert.Throws<ArgumentException>(() => account.Deposit(0m));
    }
    [Fact]
    public void Withdraw_Balance()
    {
        var account = new Account("1", "Test", 150m);
        account.Withdraw(100m);
        Assert.Equal(50m, account.Balance);
    }
    [Fact]
    public void Withdraw_Throws_InsufficientFunds()
    {
        var account = new Account("1", "Test", 150m);
        Assert.Throws<InvalidOperationException>(() => account.Withdraw(200m));
    }
    [Fact]
    public void Withdraw_Throws_AmountEqualsZero()
    {
        var account = new Account("1", "Test", 150m);
        Assert.Throws<ArgumentException>(() => account.Withdraw(0m));
    }
}