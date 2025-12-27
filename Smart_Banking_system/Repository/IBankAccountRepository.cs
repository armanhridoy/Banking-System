using Microsoft.EntityFrameworkCore;
using Smart_Banking_system.ApplicationDB;
using Smart_Banking_system.Models;

namespace Smart_Banking_system.Repository;

public interface IBankAccountRepository
{
    Task<IEnumerable<BankAccount>> GetAllBankAccountAsync(CancellationToken cancellationToken );
    Task<BankAccount>AddBankAccountAsync(BankAccount bankAccount, CancellationToken cancellationToken );
    Task<BankAccount>UpdateBankAccountAsync(BankAccount bankAccount, CancellationToken cancellationToken );
    Task<BankAccount>DeleteBankAccountAsync(int id, CancellationToken cancellationToken );
    Task<BankAccount>GetBankAccountByIdAsync(int id, CancellationToken cancellationToken );
}
public class BankaccountRepository : IBankAccountRepository
{
    private readonly ApplicationDbContext _context;
    public BankaccountRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<BankAccount> AddBankAccountAsync(BankAccount bankAccount, CancellationToken cancellationToken)
    {
        await _context.AddAsync(bankAccount);
        _context.SaveChanges();
        return bankAccount;
    }

    public async Task<BankAccount> DeleteBankAccountAsync(int id, CancellationToken cancellationToken)
    {
        var data = await _context.BankAccounts.FindAsync(id, cancellationToken);
        if(data != null)
        {
            _context.BankAccounts.Remove(data);
            _context.SaveChanges();
        }
        return null;
    }

    public async Task<IEnumerable<BankAccount>> GetAllBankAccountAsync(CancellationToken cancellationToken)
    {
        var data = await _context.BankAccounts.ToListAsync(cancellationToken);  
        if (data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<BankAccount> GetBankAccountByIdAsync(int id, CancellationToken cancellationToken)
    {
        var data = await _context.BankAccounts.FindAsync(id,cancellationToken);
        if(data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<BankAccount> UpdateBankAccountAsync(BankAccount bankAccount, CancellationToken cancellationToken)
    {
        var data = await _context.BankAccounts.FindAsync(bankAccount.Id, cancellationToken);
        if (data != null)
        {
            data.AccountNumber = bankAccount.AccountNumber;
            data.AccountHolderName = bankAccount.AccountHolderName;
            data.Balance = bankAccount.Balance;
            data.AccountType = bankAccount.AccountType;
            data.CreatedAt = bankAccount.CreatedAt;
            _context.SaveChanges();
            return data;
        }
        return null;

    }
}
