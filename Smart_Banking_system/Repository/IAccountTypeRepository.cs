using Microsoft.EntityFrameworkCore;
using Smart_Banking_system.ApplicationDB;
using Smart_Banking_system.Models;

namespace Smart_Banking_system.Repository;

public interface IAccountTypeRepository
{
    Task<IEnumerable<AccountType>> GetAllAccountTypeAsync (CancellationToken cancellationToken );
    Task<AccountType>AddAccountTypeAsync(AccountType accountType, CancellationToken cancellationToken );
    Task<AccountType>UpdateAccountTypeAsync(AccountType accountType, CancellationToken cancellationToken );
    Task<AccountType>DeleteAccountTypeAsync(int id, CancellationToken cancellationToken );
    Task<AccountType>GetAccountTypeByIdAsync(int id, CancellationToken cancellationToken );
}
public class AccountTypeRepository : IAccountTypeRepository
{
    private readonly ApplicationDbContext _context;
    public AccountTypeRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<AccountType> AddAccountTypeAsync(AccountType accountType, CancellationToken cancellationToken)
    {
        await _context.AddAsync(accountType);
        _context.SaveChanges();
        return accountType;
    }
    public async Task<AccountType> DeleteAccountTypeAsync(int id, CancellationToken cancellationToken)
    {
        var data = await _context.AccountTypes.FindAsync(id, cancellationToken);
        if (data != null)
        {
            _context.AccountTypes.Remove(data);
            _context.SaveChanges();
        }
        return null;
    }
    public async Task<IEnumerable<AccountType>> GetAllAccountTypeAsync(CancellationToken cancellationToken)
    {
       var data = await _context.AccountTypes.ToListAsync(cancellationToken);
        if(data != null)
         {
          return data;
        }
        return null;
    }
    public async Task<AccountType> GetAccountTypeByIdAsync(int id, CancellationToken cancellationToken)
    {
        var data = await _context.AccountTypes.FindAsync(id,cancellationToken);
        if(data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<AccountType> UpdateAccountTypeAsync(AccountType accountType, CancellationToken cancellationToken)
    {
        var data = await _context.AccountTypes.FindAsync(accountType.Id, cancellationToken);
        if (data != null)
        {
            data.TypeName = accountType.TypeName;
            data.Description = accountType.Description;
            _context.SaveChanges();
            return data;
        }
        return null;
    }
}
