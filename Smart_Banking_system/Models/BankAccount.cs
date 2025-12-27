namespace Smart_Banking_system.Models;

public class BankAccount
{
    public int Id { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public string AccountHolderName { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public string AccountType { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } =DateTime.Now;
    // Foreign Key to Customer
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    // One BankAccount → Many Transactions
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
