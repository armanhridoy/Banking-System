using Microsoft.EntityFrameworkCore;
using Smart_Banking_system.ApplicationDB;
using Smart_Banking_system.Models;

namespace Smart_Banking_system.Repository;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllEmployeeAsync(CancellationToken cancellationToken );  
    Task<Employee>AddEmployeeAsync(Employee employee, CancellationToken cancellationToken );  
    Task<Employee>UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken );  
    Task<Employee>DeleteEmployeeAsync(int id, CancellationToken cancellationToken );  
    Task<Employee>GetEmployeeByIdAsync(int id, CancellationToken cancellationToken );
}
public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;
    public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Employee> AddEmployeeAsync(Employee employee, CancellationToken cancellationToken)
    {
        await _context.AddAsync(employee);
        _context.SaveChanges();
        return employee;
    }

    public async Task<Employee> DeleteEmployeeAsync(int id, CancellationToken cancellationToken)
    {
        var data = await _context.Employees.FindAsync(id, cancellationToken);
        if (data != null)
        {
            _context.Employees.Remove(data);
            _context.SaveChanges();
        }
        return null;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeeAsync(CancellationToken cancellationToken)
    {
       var data = await _context.Employees.ToListAsync(cancellationToken);
        if(data != null)
         {
          return data;
        }
        return null;
    }

    public async Task<Employee> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken)
    {
        var data = await _context.Employees.FindAsync(id,cancellationToken);
        if(data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<Employee> UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken)
    {
        var data = await _context.Employees.FindAsync(employee.Id, cancellationToken);
        if(data != null)
        {
            data.Name = employee.Name;
            data.Email = employee.Email;
            data.Phone = employee.Phone;
            data.Position = employee.Position;
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }
}
