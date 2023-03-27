using Contracts.DTOs.Expenses;
using Contracts.Responses.Expenses;
using Contracts.Responses.General;

namespace Services.Abstractions;

public interface IExpenseService
{
    Task<ExpenseResponse> CreateExpenseAsync(CreateExpenseDto dto, CancellationToken cancellationToken = default);

    Task<ExpenseResponse> GetExpenseByIdAsync(Guid expenseId, CancellationToken cancellationToken = default);

    Task<PagedResponse<ExpenseResponse>> GetPagedExpensesAsync(GetUserExpensesDto dto,
        CancellationToken cancellationToken = default);
}