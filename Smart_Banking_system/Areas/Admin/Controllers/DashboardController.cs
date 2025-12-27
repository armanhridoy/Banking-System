using Microsoft.AspNetCore.Mvc;

namespace Smart_Banking_system.Areas.Admin.Controllers;

public class DashboardController : Controller
{
    [Area("Admin")]
    public IActionResult Index()
    {
        return View();
    }
}
