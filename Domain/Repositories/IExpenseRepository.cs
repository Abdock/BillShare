using Domain.Models;

namespace Domain.Repositories;

public interface IExpenseRepository
{
    Task AddExpenseAsync(Expense expense, CancellationToken cancellationToken = default);
    Task<Expense> GetExpenseByIdAsync(Guid expenseId, CancellationToken cancellationToken = default);
    
}