using Microsoft.AspNetCore.Mvc;
using Smart_Banking_system.Models;
using Smart_Banking_system.Repository;

namespace Smart_Banking_system.Areas.Admin.Controllers;
[Area("Admin")]
public class BankAccountsController : Controller
{
    private readonly IBankAccountRepository _bankAccountRepository;
    public BankAccountsController(IBankAccountRepository bankAccountRepository)
    {
        _bankAccountRepository = bankAccountRepository;
    }   
    public async Task <IActionResult> Index(CancellationToken cancellationToken)
    {
        var data =await _bankAccountRepository.GetAllBankAccountAsync(cancellationToken);
        return View(data);
    }
    [HttpGet]
    public async Task<IActionResult> CreateOrEdit(int id, CancellationToken cancellationToken)
    {
        if(id == 0)
        {
            return View(new BankAccount());
        }
        var data = await _bankAccountRepository.GetBankAccountByIdAsync(id, cancellationToken);
        if(data == null)
        {
            return NotFound();
        }
        return View(data);
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrEdit(BankAccount bankAccount, CancellationToken cancellationToken)
    {
        if(bankAccount.Id == 0)
        {
            await _bankAccountRepository.AddBankAccountAsync(bankAccount, cancellationToken);
            return RedirectToAction("Index");
        }
        else
        {
            await _bankAccountRepository.UpdateBankAccountAsync(bankAccount, cancellationToken);
            return RedirectToAction("Index");
        }
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var data = await _bankAccountRepository.GetBankAccountByIdAsync(id, cancellationToken);
        if(data == null)
        {
            return NotFound();
        }
        return View(data);
    }
    [HttpGet]
    public async Task<IActionResult> Delete (int id,CancellationToken cancellationToken)
    {
        var data = await _bankAccountRepository.DeleteBankAccountAsync(id, cancellationToken);
        if(data == null)
        {
            return NotFound();
        }
        await _bankAccountRepository.DeleteBankAccountAsync(id, cancellationToken);
        return RedirectToAction("Index");
    }
}
