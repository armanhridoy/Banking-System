using Microsoft.AspNetCore.Mvc;
using Smart_Banking_system.Models;
using Smart_Banking_system.Repository;

namespace Smart_Banking_system.Controllers;

public class AccountTypeController : Controller
{
    private readonly IAccountTypeRepository _accountTypeRepository;
    public AccountTypeController (IAccountTypeRepository accountTypeRepository)
    {
        _accountTypeRepository = accountTypeRepository;
    }


    public async Task <IActionResult> Index(CancellationToken cancellationToken)
    {
        var data = await _accountTypeRepository.GetAllAccountTypeAsync(cancellationToken);
        return View(data);
    }
    [HttpGet]
    public async Task<IActionResult> CreateOrEdit(int id,CancellationToken cancellationToken)
    {
        if (id == 0)
        {
            return View (new AccountType());
        }
        var data = await _accountTypeRepository.GetAccountTypeByIdAsync(id, cancellationToken);
        if (data == null)
        {
            return NotFound();
        }
        return View(data);
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrEdit (AccountType accountType , CancellationToken cancellationToken)
    {
        if (accountType.Id ==0)
        {
            await _accountTypeRepository.AddAccountTypeAsync(accountType, cancellationToken);
            return RedirectToAction("Index");
        }
        else
        {
            await _accountTypeRepository.UpdateAccountTypeAsync(accountType, cancellationToken);
            return RedirectToAction("Index");
        }
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var data = await _accountTypeRepository.GetAccountTypeByIdAsync(id, cancellationToken);
        if (data == null)
        {
            return NotFound();
        }
        return View(data);
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var data = await _accountTypeRepository.DeleteAccountTypeAsync(id, cancellationToken);
        if (data != null)
        {
            await _accountTypeRepository.DeleteAccountTypeAsync(id, cancellationToken);
            return RedirectToAction("Index");
        }
        return NotFound();  
    }
}
