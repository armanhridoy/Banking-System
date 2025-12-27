namespace Smart_Banking_system.Models;

public class Loan
{
    public int Id { get; set; }
    // Foreign Key to Customer
    public int CustomerId { get; set; }
    public string LoanType { get; set; }
    public decimal LoanAmount { get; set; }
    public double InterestRate { get; set; }
    public DateOnly IssueDate { get; set; }
    public DateOnly DueDate { get; set; }
    public string Status { get; set; }
    public Customer Customer { get; set; }
    public ICollection<LoanPayment> LoanPayments { get; set; } = new List<LoanPayment>();
}