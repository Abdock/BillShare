using Domain.Models;

namespace Domain.Repositories;

public interface IExpenseRepository
{
    Task LoadRelatedDataAsync(Expense expense, CancellationToken cancellationToken = default);
    Task AddExpenseAsync(Expense expense, CancellationToken cancellationToken = default);
    Task<Expense> GetExpenseByIdAsync(Guid expenseId, Guid customerId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Expense>> GetPagedExpensesAsync(Guid customerId, int skipCount, int takeCount,
        CancellationToken cancellationToken = default);

    Task LockExpenseAsync(Guid customerId, Guid expenseId, CancellationToken cancellationToken = default);

    Task UnlockExpenseAsync(Guid customerId, Guid expenseId, CancellationToken cancellationToken = default);

    Task<int> TotalCountAsync(Guid customerId, CancellationToken cancellationToken = default);

    Task AddParticipantAsync(Guid expenseId, Guid customerId, CancellationToken cancellationToken = default);

    Task DeleteParticipantAsync(Guid expenseId, Guid participantId, CancellationToken cancellationToken = default);

    Task SelectItemAsync(Guid expenseId, Guid expenseItemId, Guid customerId,
        CancellationToken cancellationToken = default);

    Task UnselectItemAsync(Guid expenseId, Guid expenseItemId, Guid customerId,
        CancellationToken cancellationToken = default);

    Task DeleteItemAsync(Guid expenseId, Guid expenseItemId, Guid customerId,
        CancellationToken cancellationToken = default);

    Task AddItemAsync(Guid expenseId, Guid customerId, ExpenseItem expenseItem,
        CancellationToken cancellationToken = default);
}