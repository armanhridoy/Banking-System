using Microsoft.AspNetCore.Mvc;
using Smart_Banking_system.Models;
using Smart_Banking_system.Repository;

namespace Smart_Banking_system.Areas.Admin.Controllers;
[Area("Admin")]
public class NotificationController : Controller
{
    private readonly INotificationRepository _notificationRepository;
    public NotificationController (INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }   
    public async Task <IActionResult> Index(CancellationToken cancellationToken)
    {
        var data = await _notificationRepository.GetAllNotificationsAsync(cancellationToken);
        return View(data);
    }
    [HttpGet]
    public async Task<IActionResult> CreateOrEdit(int id,CancellationToken cancellationToken)
    {
        if (id == 0)
        {
            return View (new Notification());
        }
        var data = await _notificationRepository.GetNotificationByIdAsync(id, cancellationToken);
        if (data == null)
        {
            return NotFound();
        }
        return View(data);
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrEdit (Notification notification , CancellationToken cancellationToken)
    {
        if (notification.Id ==0)
        {
            await _notificationRepository.AddNotificationAsync(notification, cancellationToken);
            return RedirectToAction("Index");
        }
        else
        {
            await _notificationRepository.UpdateNotificationAsync(notification, cancellationToken);
            return RedirectToAction("Index");
        }
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var data = await _notificationRepository.GetNotificationByIdAsync(id, cancellationToken);
        if (data == null)
        {
            return NotFound();
        }
        return View(data);
    }
 
    [HttpGet]
    public async Task<IActionResult> Delete (int id ,CancellationToken cancellationToken)
    {
        var data = await _notificationRepository.DeleteNotificationAsync(id, cancellationToken);
        if (data != null) 
        {
            await _notificationRepository.DeleteNotificationAsync(id, cancellationToken);
            return RedirectToAction("Index");
        }
        return NotFound();  
    }
}
