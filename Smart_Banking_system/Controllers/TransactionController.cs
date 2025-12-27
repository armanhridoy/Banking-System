using Microsoft.AspNetCore.Mvc;
using Smart_Banking_system.Models;
using Smart_Banking_system.Repository;

namespace Smart_Banking_system.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task <IActionResult> Index(CancellationToken cancellationToken)
        {
            var data = await _transactionRepository.GetAllTransactionsAsync(cancellationToken);
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> CreateOrEdit (int id, CancellationToken cancellationToken)
        {
            if (id ==0)
            {
                return View (new Transaction());
            }
            var data = await _transactionRepository.GetTransactionByIdAsync(id, cancellationToken);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrEdit( Transaction transaction , CancellationToken cancellationToken)
        {
            if(transaction.Id == 0)
            {
                await _transactionRepository.AddTransactionAsync(transaction, cancellationToken);
                return RedirectToAction("Index");
            }
            else
            {
                await _transactionRepository.UpdateTransactionAsync(transaction, cancellationToken);
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var data = await _transactionRepository.GetTransactionByIdAsync(id, cancellationToken);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Delete ( int id , CancellationToken cancellationToken)
        {
            var data = await _transactionRepository.DeleteTransactionAsync(id, cancellationToken);
            if(data == null)
            {
                return NotFound();
            }
            await _transactionRepository.DeleteTransactionAsync(id, cancellationToken);
            return RedirectToAction("Index");
        }
    }
}
