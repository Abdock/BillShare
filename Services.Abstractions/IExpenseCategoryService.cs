using Contracts.DTOs.ExpenseCategories;

namespace Services.Abstractions;

public interface IExpenseCategoryService
{
    Task AddExpenseCategoryAsync(CreateExpenseCategoryDto dto, CancellationToken cancellationToken = default);
}