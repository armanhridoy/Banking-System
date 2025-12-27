namespace Smart_Banking_system.Models;

public class LoanPayment
{
    public Guid Id { get; set; }
    // Foreign Key to Loan
    public int LoanId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal RemainingBalance { get; set; }
    public Loan Loan { get; set; }
}
