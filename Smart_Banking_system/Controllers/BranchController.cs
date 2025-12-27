using Microsoft.AspNetCore.Mvc;
using Smart_Banking_system.Models;
using Smart_Banking_system.Repository;

namespace Smart_Banking_system.Controllers
{
    public class BranchController : Controller
    {
        private readonly IBranchRepository _branchRepository;
        public BranchController(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }
        public async Task< IActionResult> Index(CancellationToken cancellationToken)
        {
            var data = await _branchRepository.GetAllBranchesAsync(cancellationToken);
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult>CreateOrEdit(int id,CancellationToken cancellationToken)
        {
            if (id == 0)
                {
                return View(new Branch());
                }
            var data = await _branchRepository.GetBranchByIdAsync(id, cancellationToken);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult>CreateOrEdit(Branch branch, CancellationToken cancellationToken)
        {
            if (branch.Id ==0)
            {
                await _branchRepository.AddBranchAsync(branch, cancellationToken);
                return RedirectToAction("Index");
            }
            else
            {
                await _branchRepository.UpdateBranchAsync(branch, cancellationToken);
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var data = await _branchRepository.GetBranchByIdAsync(id, cancellationToken);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var data = await _branchRepository.DeleteBranchAsync(id, cancellationToken);
            if (data == null)
            {
                return NotFound();
            }
            await _branchRepository.DeleteBranchAsync(id, cancellationToken);
            return RedirectToAction("Index");
        }
    }
}
