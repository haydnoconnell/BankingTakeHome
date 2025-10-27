# BankingTakeHome

A small, in-memory banking service that supports **account creation**, **deposit**, **withdraw**, **transfer**, and **balance lookup**.  
The focus is on clear business rules (no overdrafts, positive amounts only) and simple, testable design.

## How to run
```bash
# run tests
dotnet test

# run console demo
dotnet run --project BankingTakeHome.Demo
```
## Design Choices

**Separation of concerns:**
- The project is split into three layers, Domain, Application and Infrastructure.
This keeps the logic modular, testable and easy to extend with a real database in the future. 

**Single Currency:**
- All balances are treated as one currency for simplicity. Multiple currencies and FX were out of scope.

**Clear Business Rules:** 
- Positive amounts only for all operations
- No overdrafts
- No self-transfer

**Error Handling**
- Invalid inputs result in 'ArgumentException'
- Rule violations like insufficient funds result in 'InvalidOperationException'
- Missing account result in 'InvalidOperationException' 

**Testing**
- The solution includes xUnit tests covering all key scenarios:
- Create, Deposit, Withdraw, Balance
- No Overdrafts
- Transfer(success and failure cases)
- No self-transfer

## Future Improvements
- Add Database
- Support multiple currencies and FX rates
- Add concurrent handling (per-account locks)
- Implement API using endpoints