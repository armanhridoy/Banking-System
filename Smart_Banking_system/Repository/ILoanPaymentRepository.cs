using Microsoft.EntityFrameworkCore;
using Smart_Banking_system.ApplicationDB;
using Smart_Banking_system.Models;

namespace Smart_Banking_system.Repository;

public interface ILoanPaymentRepository
{
    Task<IEnumerable<LoanPayment>> GetAllLoanPaymentsAsync(CancellationToken cancellationToken );
    Task<LoanPayment>AddLoanPaymentAsync(LoanPayment loanPayment, CancellationToken cancellationToken );
    Task<LoanPayment>UpdateLoanPaymentAsync(LoanPayment loanPayment, CancellationToken cancellationToken );
    Task<LoanPayment>DeleteLoanPaymentAsync(Guid id, CancellationToken cancellationToken );
    Task<LoanPayment>GetLoanPaymentByIdAsync(Guid id, CancellationToken cancellationToken );
}
public class LoanPaymentRepository : ILoanPaymentRepository
{
    private readonly ApplicationDbContext _context;
    public LoanPaymentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<LoanPayment> AddLoanPaymentAsync(LoanPayment loanPayment, CancellationToken cancellationToken)
    {
        await _context.AddAsync(loanPayment);
        _context.SaveChanges();
        return loanPayment;
    }
    public async Task<LoanPayment> DeleteLoanPaymentAsync(Guid id, CancellationToken cancellationToken)
    {
        var data = await _context.LoanPayments.FindAsync(id, cancellationToken);
        if (data != null)
        {
            _context.LoanPayments.Remove(data);
            _context.SaveChanges();
        }
        return null;
    }
    public async Task<IEnumerable<LoanPayment>> GetAllLoanPaymentsAsync(CancellationToken cancellationToken)
    {
       var data = await _context.LoanPayments.ToListAsync(cancellationToken);
        if(data != null)
         {
          return data;
        }
        return null;
    }
    public async Task<LoanPayment> GetLoanPaymentByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var data = await _context.LoanPayments.FindAsync(id,cancellationToken);
        if(data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<LoanPayment> UpdateLoanPaymentAsync(LoanPayment loanPayment, CancellationToken cancellationToken)
    {
        var data =  await _context.LoanPayments.FindAsync(loanPayment.Id, cancellationToken);
        if (data != null)
        {
            data.PaymentDate = loanPayment.PaymentDate;
            data.AmountPaid = loanPayment.AmountPaid;
            data.RemainingBalance = loanPayment.RemainingBalance;
            _context.SaveChanges();
            return data;
        }
        return null;
    }
}
