using Microsoft.AspNetCore.Mvc;
using Smart_Banking_system.Models;
using Smart_Banking_system.Repository;

namespace Smart_Banking_system.Areas.Admin.Controllers;
[Area("Admin")]
public class LoanController : Controller
{
    private readonly ILoanRepository _loanRepository;
    public LoanController(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var data = await _loanRepository.GetAllLoansAsync(cancellationToken);
        return View(data);
    }
    [HttpGet]
    public async Task<IActionResult> CreateOrEdit(int id, CancellationToken cancellationToken)
    {
        if (id == 0)
        {
            return View(new Loan());
        }
        var data = await _loanRepository.GetLoanByIdAsync(id, cancellationToken);
        if (data == null)
        {
            return NotFound();
        }
        return View(data);
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrEdit(Loan loan, CancellationToken cancellationToken)
    {
        if (loan.Id == 0)
        {
            await _loanRepository.AddLoanAsync(loan, cancellationToken);
            return RedirectToAction("Index");
        }
        else
        {
            await _loanRepository.UpdateLoanAsync(loan, cancellationToken);
            return RedirectToAction("Index");
        }
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var data = await _loanRepository.GetLoanByIdAsync(id, cancellationToken);
        if (data == null)
        {
            return NotFound();
        }
        return View(data);
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var data = await _loanRepository.GetLoanByIdAsync(id, cancellationToken);
        if (data == null)
        {
            await _loanRepository.DeleteLoanAsync(id, cancellationToken);
            return RedirectToAction("Index");
        }
        return NotFound();  
       
    }
    
}
