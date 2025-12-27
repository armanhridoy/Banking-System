using Microsoft.EntityFrameworkCore;
using Smart_Banking_system.ApplicationDB;
using Smart_Banking_system.Models;

namespace Smart_Banking_system.Repository;

public interface IBranchRepository
{
    Task<IEnumerable<Branch>> GetAllBranchesAsync(CancellationToken cancellationToken);
    Task<Branch> AddBranchAsync(Branch branch, CancellationToken cancellationToken);
    Task<Branch> UpdateBranchAsync(Branch branch, CancellationToken cancellationToken);
    Task<Branch> DeleteBranchAsync(int Id, CancellationToken cancellationToken);
    Task<Branch> GetBranchByIdAsync(int Id, CancellationToken cancellationToken);

}
public class BranchRepository : IBranchRepository
{
    private readonly ApplicationDbContext _context;
    public BranchRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Branch> AddBranchAsync(Branch branch, CancellationToken cancellationToken)
    {
        await _context.AddAsync(branch, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return branch;
    }

    public async Task<Branch> DeleteBranchAsync(int Id, CancellationToken cancellationToken)
    {
        var data =await _context.Branches.FindAsync(Id, cancellationToken);
        if(data != null)
        {
            _context.Branches.Remove(data);
            _context.SaveChanges();
        }
        return null;
    }

    public async Task<IEnumerable<Branch>> GetAllBranchesAsync(CancellationToken cancellationToken)
    {
        var data = await _context.Branches.ToListAsync(cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<Branch> GetBranchByIdAsync(int Id, CancellationToken cancellationToken)
    {
        var data = await _context.Branches.FindAsync(Id, cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<Branch> UpdateBranchAsync(Branch branch, CancellationToken cancellationToken)
    {
        var data = await _context.Branches.FindAsync(branch.Id, cancellationToken);
        if (data != null)
        {
            data.BranchName = branch.BranchName;
            data.BranchCode = branch.BranchCode;
            data.Address = branch.Address;
            data.PhoneNumber = branch.PhoneNumber;
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }
}
