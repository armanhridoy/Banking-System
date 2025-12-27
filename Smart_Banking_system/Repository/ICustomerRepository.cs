using Microsoft.EntityFrameworkCore;
using Smart_Banking_system.ApplicationDB;
using Smart_Banking_system.Models;

namespace Smart_Banking_system.Repository;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync(CancellationToken cancellationToken );
    Task<Customer>AddCustomerAsync(Customer customer, CancellationToken cancellationToken );
    Task<Customer>UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken );
    Task<Customer>DeleteCustomerAsync(int id, CancellationToken cancellationToken );
    Task<Customer>GetCustomerByIdAsync(int id, CancellationToken cancellationToken );
}
public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;
    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Customer> AddCustomerAsync(Customer customer, CancellationToken cancellationToken)
    {
        await _context.AddAsync(customer);
        _context.SaveChanges();
        return customer;
    }
    public async Task<Customer> DeleteCustomerAsync(int id, CancellationToken cancellationToken)
    {
        var data = await _context.Customers.FindAsync(id, cancellationToken);
        if (data != null)
        {
            _context.Customers.Remove(data);
            _context.SaveChanges();
        }
        return null;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync(CancellationToken cancellationToken)
    {
        var data = await _context.Customers.ToListAsync(cancellationToken);
        if(data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<Customer> GetCustomerByIdAsync(int id, CancellationToken cancellationToken)
    {
        var data = await _context.Customers.FindAsync(id,cancellationToken);
        if(data != null)
        {
            return data;
        }
        return null;
    }

   

    public async Task<Customer> UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken)
    {
        var data = await _context.Customers.FindAsync(customer.Id, cancellationToken);
        if(data != null)
        {
            data.Name = customer.Name;
            data.Email = customer.Email;
            data.Phone = customer.Phone;
            data.NID = customer.NID;
            data.Address = customer.Address;
            data.DateOfBirth = customer.DateOfBirth;
            data.DateTime = customer.DateTime;
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }
}
