using Microsoft.EntityFrameworkCore;
using Smart_Banking_system.ApplicationDB;
using Smart_Banking_system.Models;

namespace Smart_Banking_system.Repository;

public interface ILoanRepository
{
    Task<IEnumerable<Loan>> GetAllLoansAsync(CancellationToken cancellationToken);
    Task<Loan> AddLoanAsync(Loan loan, CancellationToken cancellationToken);
    Task<Loan> UpdateLoanAsync(Loan loan, CancellationToken cancellationToken);
    Task<Loan> DeleteLoanAsync(int Id, CancellationToken cancellationToken);
    Task<Loan> GetLoanByIdAsync(int Id, CancellationToken cancellationToken);
}
public class LoanRepository : ILoanRepository
{
    private readonly ApplicationDbContext _context;
    public LoanRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Loan> AddLoanAsync(Loan loan, CancellationToken cancellationToken)
    {
        await _context.AddAsync(loan, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return loan;
    }
    public async Task<Loan> DeleteLoanAsync(int Id, CancellationToken cancellationToken)
    {
        var data =await _context.Loans.FindAsync(Id, cancellationToken);
        if(data != null)
        {
            _context.Loans.Remove(data);
            _context.SaveChanges();
        }
        return null;
    }
    public async Task<IEnumerable<Loan>> GetAllLoansAsync(CancellationToken cancellationToken)
    {
        var data = await _context.Loans.ToListAsync(cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<Loan> GetLoanByIdAsync(int Id, CancellationToken cancellationToken)
    {
        var data = await _context.Loans.FindAsync(Id, cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<Loan> UpdateLoanAsync(Loan loan, CancellationToken cancellationToken)
    {
        var data = await _context.Loans.FindAsync(loan.Id, cancellationToken);
        if(data != null)
        {
            data.LoanType = loan.LoanType;
            data.LoanAmount = loan.LoanAmount;
            data.InterestRate = loan.InterestRate;
            data.IssueDate = loan.IssueDate;
            data.DueDate = loan.DueDate;
            data.Status = loan.Status;
            await  _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }
}


