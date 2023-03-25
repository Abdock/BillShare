using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ExpenseTypeRepository : IExpenseTypeRepository
{
    private readonly AppDbContext _context;

    public ExpenseTypeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExpenseType>> GetAllExpenseTypesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.ExpenseTypes.ToListAsync(cancellationToken);
    }
}