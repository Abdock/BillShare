using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database.Context;

namespace Infrastructure.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly AppDbContext _context;

    public ExpenseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddExpenseAsync(Expense expense, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Expense> GetExpenseByIdAsync(Guid expenseId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}