namespace Smart_Banking_system.Models;

public class Branch
{
    public int Id { get; set; }
    public string BranchName{ get; set; }
    public string BranchCode { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CreateDate { get; set; }= DateTime.Now;
    // One Branch → Many Employees
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();

}
