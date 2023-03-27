using Domain.Models;

namespace Domain.Repositories;

public interface IExpenseRepository
{
    Task AddExpenseAsync(Expense expense, CancellationToken cancellationToken = default);
    Task<Expense> GetExpenseByIdAsync(Guid expenseId, Guid customerId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Expense>> GetPagedExpensesAsync(Guid customerId, int skipCount, int takeCount,
        CancellationToken cancellationToken = default);

    Task LockExpenseAsync(Guid customerId, Guid expenseId, CancellationToken cancellationToken = default);

    Task UnlockExpenseAsync(Guid customerId, Guid expenseId, CancellationToken cancellationToken = default);
}