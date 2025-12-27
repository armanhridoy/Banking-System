using Microsoft.AspNetCore.Mvc;
using Smart_Banking_system.Models;
using Smart_Banking_system.Repository;

namespace Smart_Banking_system.Areas.Admin.Controllers;

    [Area("Admin")]
public class EmployeeController : Controller
{
   
    private readonly IEmployeeRepository _employeeRepository;
    public EmployeeController (IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }   
    public async Task< IActionResult> Index(CancellationToken cancellationToken)
    {
        var data = await _employeeRepository.GetAllEmployeeAsync(cancellationToken);
        return View(data);
    }
    [HttpGet]
    public async Task<IActionResult> CreateOrEdit(int id,CancellationToken cancellationToken)
    {
        if (id == 0)
        {
            return View(new Employee());
        }
        var data = await _employeeRepository.GetEmployeeByIdAsync(id, cancellationToken);
        if (data == null)
        {
            return NotFound();
        }
        return View(data);
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrEdit (Employee employee , CancellationToken cancellationToken)
    {
        if (employee.Id ==0)
        {
            await _employeeRepository.AddEmployeeAsync(employee, cancellationToken);
            return RedirectToAction("Index");
        }
        else
        {
            await _employeeRepository.UpdateEmployeeAsync(employee, cancellationToken);
            return RedirectToAction("Index");
        }
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var data = await _employeeRepository.GetEmployeeByIdAsync(id, cancellationToken);
        if (data == null)
        {
            return NotFound();
        }
        return View(data);
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var data = await _employeeRepository.DeleteEmployeeAsync(id, cancellationToken);
        if (data != null)
        {
            await _employeeRepository.DeleteEmployeeAsync(id, cancellationToken);
            return RedirectToAction("Index");
        }
        return NotFound();
    }
}

