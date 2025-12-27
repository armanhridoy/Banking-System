using Microsoft.EntityFrameworkCore;
using Smart_Banking_system.ApplicationDB;
using Smart_Banking_system.Models;

namespace Smart_Banking_system.Repository;

public interface IAdminUserRepository
{
    Task<IEnumerable<AdminUser>> GetAllAdminUsersAsync(CancellationToken cancellationToken );
    Task<AdminUser>AddAdminUserAsync(AdminUser adminUser, CancellationToken cancellationToken );
    Task<AdminUser>UpdateAdminUserAsync(AdminUser adminUser, CancellationToken cancellationToken );
    Task<AdminUser>DeleteAdminUserAsync(int id, CancellationToken cancellationToken );
    Task<AdminUser>GetAdminUserByIdAsync(int id, CancellationToken cancellationToken );
}

public class AdminUserRepository : IAdminUserRepository
{
    public readonly ApplicationDbContext _context;
    public AdminUserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<AdminUser> AddAdminUserAsync(AdminUser adminUser, CancellationToken cancellationToken)
    {
        await _context.AddAsync(adminUser);
        _context.SaveChanges();
        return adminUser;
    }
    public async Task<AdminUser> DeleteAdminUserAsync(int id, CancellationToken cancellationToken)
    {
        var data =await _context.AdminUsers.FindAsync(id, cancellationToken);
        if (data != null)
        {
            _context.AdminUsers.Remove(data);
            _context.SaveChanges();
        }
        return null;
    }

    public Task<AdminUser> GetAdminUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<AdminUser>> GetAllAdminUsersAsync(CancellationToken cancellationToken)
    {
        var data = await _context.AdminUsers.ToListAsync(cancellationToken);
        if(data != null)
         {
          return data;
        }
        return null;
    }

    public async Task<AdminUser> UpdateAdminUserAsync(AdminUser adminUser, CancellationToken cancellationToken)
    {
        var data = await _context.AdminUsers.FindAsync(adminUser.Id, cancellationToken);
        if (data != null)
        {
            data.Username = adminUser.Username;
            data.Password = adminUser.Password;
            data.Email = adminUser.Email;
            data.CreatedAt = adminUser.CreatedAt;
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }
}
