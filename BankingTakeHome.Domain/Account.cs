namespace BankingTakeHome.Domain;

public class Account
{
    public string Id { get; }
    public string OwnerName { get; }
    public decimal Balance { get; private set; }

    public Account(string id, string ownerName, decimal initialDeposit)
    {
        if (string.IsNullOrWhiteSpace(ownerName))
        {
            throw new ArgumentException("Owner Name cannot be null or empty");
        }

        if (initialDeposit < 0)
        {
            throw new ArgumentException("Initial Deposit cannot be negative");
        }
        
        Id = id;
        OwnerName = ownerName;
        Balance = initialDeposit;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Deposit amount must be greater than zero");
        }
        
        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Withdrawal amount must be greater than zero");
        }

        if (amount > Balance)
        {
            throw new InvalidOperationException("Insufficient funds, no overdraft allowed");
        }
        
        Balance -= amount;
    }
}