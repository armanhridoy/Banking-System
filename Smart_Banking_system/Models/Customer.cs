using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Smart_Banking_system.Models;

public class Customer
{
    public int Id { get; set; } 

    [Required, StringLength(100)]
    public string Name { get; set; }

    [StringLength(100)]
    public string Email { get; set; }

    [Required, StringLength(15)]
    public string Phone { get; set; }

    [Required, StringLength(20)]
    public string NID { get; set; }

    [Required, StringLength(200)]
    public string Address { get; set; }
    [Required, StringLength(10)]

    public string DateOfBirth { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;

    // One Customer → Many BankAccounts
    public ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}

