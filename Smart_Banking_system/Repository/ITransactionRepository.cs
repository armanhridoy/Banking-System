using Microsoft.EntityFrameworkCore;
using Smart_Banking_system.ApplicationDB;
using Smart_Banking_system.Models;

namespace Smart_Banking_system.Repository;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> GetAllTransactionsAsync(CancellationToken cancellationToken);
    Task<Transaction> AddTransactionAsync(Transaction transaction, CancellationToken cancellationToken);    
    Task<Transaction> UpdateTransactionAsync(Transaction transaction, CancellationToken cancellationToken);
    Task<Transaction> DeleteTransactionAsync(int id, CancellationToken cancellationToken);
    Task<Transaction> GetTransactionByIdAsync(int id, CancellationToken cancellationToken);
}
public class TransactionRepository : ITransactionRepository
{
       private readonly ApplicationDbContext _context;
    public TransactionRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Transaction> AddTransactionAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        await _context.AddAsync(transaction, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return transaction;
    }

    public async Task<Transaction> DeleteTransactionAsync(int id, CancellationToken cancellationToken)
    {
        var data = await _context.Transactions.FindAsync(id, cancellationToken);
        if (data != null)
        {
            _context.Transactions.Remove(data);
            await _context.SaveChangesAsync();
            return data;
        }
        return null;
    }


    public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync(CancellationToken cancellationToken)
    {
        var data = await _context.Transactions.ToListAsync(cancellationToken);
        if(data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<Transaction> GetTransactionByIdAsync(int id, CancellationToken cancellationToken)
    {
        var data = await _context.Transactions.FindAsync(id,cancellationToken);
        if(data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<Transaction> UpdateTransactionAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        var data = await _context.Transactions.FindAsync(transaction.Id, cancellationToken);
        if(data != null)
        {
            data.TransactionType = transaction.TransactionType;
            data.Amount = transaction.Amount;
            data.Date = transaction.Date;
            data.Description = transaction.Description;
            data.FromAccountId = transaction.FromAccountId;
            data.ToAccountId = transaction.ToAccountId;
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }
}
