namespace Smart_Banking_system.Models;

public class Transaction
{
    public int Id { get; set; }
    // Foreign Key to BankAccount
    public int BankAccountId { get; set; }
    public string TransactionType { get; set; } // e.g., "Deposit", "Withdrawal", "Transfer"
    public decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public string Description { get; set; }
    public string FromAccountId { get; set; }
    public string ToAccountId { get; set; }
   
    public BankAccount BankAccount { get; set; }
}
