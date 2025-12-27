using Microsoft.AspNetCore.Mvc;
using Smart_Banking_system.Models;
using Smart_Banking_system.Repository;

namespace Smart_Banking_system.Controllers;

public class AdminUserController : Controller
{
    private readonly IAdminUserRepository  _adminUserRepository;
    public AdminUserController(IAdminUserRepository adminUserRepository)
    {
        _adminUserRepository = adminUserRepository;
    }   
    public async Task <IActionResult> Index(CancellationToken cancellationToken)
    {
        var data = await _adminUserRepository.GetAllAdminUsersAsync(cancellationToken);
        return View(data);
    }
    [HttpGet]
    public async Task<IActionResult> CreateOrEdit(int id,CancellationToken cancellationToken)
    {
        if (id == 0)
        {
            return View (new AdminUser());
        }
        var data = await _adminUserRepository.GetAdminUserByIdAsync(id, cancellationToken);
        if (data == null)
        {
            return NotFound();
        }
        return View(data);
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrEdit (AdminUser adminUser , CancellationToken cancellationToken)
    {
        if (adminUser.Id ==0)
        {
            await _adminUserRepository.AddAdminUserAsync(adminUser, cancellationToken);
            return RedirectToAction("Index");
        }
        else
        {
            await _adminUserRepository.UpdateAdminUserAsync(adminUser, cancellationToken);
            return RedirectToAction("Index");
        }
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var data = await _adminUserRepository.GetAdminUserByIdAsync(id, cancellationToken);
        if (data == null)
        {
            return NotFound();
        }
        return View(data);
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var data = await _adminUserRepository.DeleteAdminUserAsync(id, cancellationToken);
        if (data == null)
        {
            await _adminUserRepository.DeleteAdminUserAsync(id, cancellationToken);
            return RedirectToAction("Index");
        }
        return NotFound();
    }
}
