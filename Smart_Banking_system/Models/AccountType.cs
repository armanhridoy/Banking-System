namespace Smart_Banking_system.Models;

public class AccountType
{
    public int Id { get; set; }
    public string TypeName { get; set; } = string.Empty; //Savings/Current/Business
    public string Description { get; set; } = string.Empty;
}
