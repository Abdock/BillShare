using Domain.Models;

namespace Domain.Repositories;

public interface ICustomExpenseCategoriesRepository
{
    Task AddExpenseCategoryAsync(CustomExpenseCategory expenseCategory, CancellationToken cancellationToken = default);

    Task<CustomExpenseCategory> GetExpenseCategoryByIdAsync(Guid expenseCategoryId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<CustomExpenseCategory>> GetAllCustomerExpenseCategories(Guid customerId,
        CancellationToken cancellationToken = default);

    Task ChangeExpenseCategoryNameAsync(Guid userId, Guid expenseCategoryId, string name,
        CancellationToken cancellationToken = default);
}