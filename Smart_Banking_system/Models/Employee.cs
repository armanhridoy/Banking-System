namespace Smart_Banking_system.Models;

public class Employee
{
    public int Id { get; set; }
    // Foreign Key to Branch
    public int BranchId { get; set; }//1 2 
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Position { get; set; }
    public Branch Branch { get; set; }
}
