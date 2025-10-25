using BankingTakeHome.Domain;
using BankingTakeHome.Infrastructure;
using Xunit;
namespace BankingTakeHome.Tests;

public class InMemoryAccountRepositoryTest
{
    [Fact]
    public async Task CreateAsync_Creates_Account()
    {
        var repository = new InMemoryAccountRepository();
        
        var account = new Account("1", "Test", 100m);
        await repository.CreateAsync(account);
        
        var result = await repository.GetAsync("1");
        Assert.Equal(100, result.Balance);
    }
    
    [Fact]
    public async Task GetAsync_Throws_if_NotFound()
    {
        var repository = new InMemoryAccountRepository();
        await Assert.ThrowsAsync<InvalidOperationException>(() => repository.GetAsync("null"));
    }
}