using Microsoft.AspNetCore.Mvc;
using Smart_Banking_system.Models;
using Smart_Banking_system.Repository;

namespace Smart_Banking_system.Controllers;

public class LoanPaymentController : Controller
{
    private readonly ILoanPaymentRepository _loanPaymentRepository;
    public LoanPaymentController(ILoanPaymentRepository loanPaymentRepository)
    {
        _loanPaymentRepository = loanPaymentRepository;
    }
    public async Task <IActionResult> Index(CancellationToken cancellationToken)

    {
        var data = await _loanPaymentRepository.GetAllLoanPaymentsAsync(cancellationToken);
        return View(data);
    }
    [HttpGet]
    public async Task<IActionResult> CreateOrEdit(Guid id,CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
        {
            return View(new LoanPayment());
        }
        var data = await _loanPaymentRepository.GetLoanPaymentByIdAsync(id, cancellationToken);
        if(data == null)
        {
            return NotFound();
        }
        return View(data);
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrEdit(LoanPayment loanPayment, CancellationToken cancellationToken)
    {
        if (loanPayment.Id == Guid.Empty)
        {
            await _loanPaymentRepository.AddLoanPaymentAsync(loanPayment, cancellationToken);
            return RedirectToAction("Index");
        }
        else
        {
            await _loanPaymentRepository.UpdateLoanPaymentAsync(loanPayment, cancellationToken);
            return RedirectToAction("Index");
        }
    }
    [HttpGet]
    public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken)
    {
        var data = await _loanPaymentRepository.GetLoanPaymentByIdAsync(id, cancellationToken);
        if (data == null)
        {
            return NotFound();
        }
        return View(data);
    }
    [HttpGet]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var data = await _loanPaymentRepository.DeleteLoanPaymentAsync(id, cancellationToken);
        if (data == null)
        {
            await _loanPaymentRepository.DeleteLoanPaymentAsync(id, cancellationToken);
            return RedirectToAction("Index");
        }
        return NotFound();
    }
}
