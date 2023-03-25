using Contracts.DTOs.ExpenseCategories;
using Contracts.Responses.ExpenseCategories;

namespace Services.Abstractions;

public interface IExpenseCategoryService
{
    Task<ExpenseCategoryResponse> AddExpenseCategoryAsync(CreateExpenseCategoryDto dto,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<ExpenseCategoryResponse>> GetAllUserCategoriesAsync(Guid customerId,
        CancellationToken cancellationToken = default);

    Task UpdateExpenseCategoryAsync(UpdateExpenseCategoryDto dto, CancellationToken cancellationToken = default);

    Task<ExpenseCategoryResponse> GetExpenseCategoryByIdAsync(Guid categoryId,
        CancellationToken cancellationToken = default);
}