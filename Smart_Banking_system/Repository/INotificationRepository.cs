using Microsoft.EntityFrameworkCore;
using Smart_Banking_system.ApplicationDB;
using Smart_Banking_system.Models;

namespace Smart_Banking_system.Repository;

public interface INotificationRepository
{
    Task<IEnumerable<Notification>> GetAllNotificationsAsync(CancellationToken cancellationToken );
    Task<Notification>AddNotificationAsync(Notification notification, CancellationToken cancellationToken );
    Task<Notification>UpdateNotificationAsync(Notification notification, CancellationToken cancellationToken );
    Task<Notification>DeleteNotificationAsync(int id, CancellationToken cancellationToken );
    Task<Notification>GetNotificationByIdAsync(int id, CancellationToken cancellationToken );

}
public class NotificationRepository : INotificationRepository
{
    public readonly ApplicationDbContext _context;
    public NotificationRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Notification> AddNotificationAsync(Notification notification, CancellationToken cancellationToken)
    {
        await _context.AddAsync(notification);
        _context.SaveChanges();
        return notification;
    }

    public async Task<Notification> DeleteNotificationAsync(int id, CancellationToken cancellationToken)
    {
        var data =await _context.notifications.FindAsync(id, cancellationToken);
        if (data != null)
        {
            _context.notifications.Remove(data);
            _context.SaveChanges();
        }
        return null;
    }

    public async Task<IEnumerable<Notification>> GetAllNotificationsAsync(CancellationToken cancellationToken)
    {
        var data = await _context.notifications.ToListAsync(cancellationToken);
        if(data != null)
         {
          return data;
        }
        return null;
    }

    public async Task<Notification> GetNotificationByIdAsync(int id, CancellationToken cancellationToken)
    {
        var data = await _context.notifications.FindAsync(id,cancellationToken);
        if(data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<Notification> UpdateNotificationAsync(Notification notification, CancellationToken cancellationToken)
    {
        var data = await _context.notifications.FindAsync(notification.Id, cancellationToken);
        if (data != null)
        {
            data.Title = notification.Title;
            data.Message = notification.Message;
            data.DateCreated = notification.DateCreated;
            data.IsRead = notification.IsRead;
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }
}
