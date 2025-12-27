using Microsoft.EntityFrameworkCore;
using Smart_Banking_system.Models;

namespace Smart_Banking_system.ApplicationDB;

public class ApplicationDbContext :DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<LoanPayment> LoanPayments { get; set; }
    public DbSet<AccountType> AccountTypes { get; set; }
    public DbSet<Notification> notifications { get; set; }
    public DbSet<AdminUser> AdminUsers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.BankAccounts)
            .WithOne(b => b.Customer)
            .HasForeignKey(b => b.CustomerId);

        modelBuilder.Entity<BankAccount>()
            .HasMany(b => b.Transactions)
            .WithOne(t => t.BankAccount)
            .HasForeignKey(t => t.BankAccountId);

        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Loans)
            .WithOne(l => l.Customer)
            .HasForeignKey(l => l.CustomerId);

        modelBuilder.Entity<Loan>()
            .HasMany<LoanPayment>()
            .WithOne(l => l.Loan)
            .HasForeignKey(l => l.LoanId);

        modelBuilder.Entity<Branch>()
            .HasMany(b => b.Employees)
            .WithOne(e => e.Branch)
            .HasForeignKey(e => e.BranchId);

      
    }
}
