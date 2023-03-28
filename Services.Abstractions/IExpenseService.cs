using Contracts.DTOs.ExpenseItems;
using Contracts.DTOs.ExpenseParticipants;
using Contracts.DTOs.Expenses;
using Contracts.Responses.Expenses;
using Contracts.Responses.General;

namespace Services.Abstractions;

public interface IExpenseService
{
    Task<ExpenseResponse> CreateExpenseAsync(CreateExpenseDto dto, CancellationToken cancellationToken = default);

    Task<ExpenseResponse> GetExpenseByIdAsync(GetExpenseByIdDto dto, CancellationToken cancellationToken = default);

    Task<PagedResponse<ExpenseResponse>> GetPagedExpensesAsync(GetUserExpensesDto dto,
        CancellationToken cancellationToken = default);

    Task LockExpense(LockExpenseDto dto, CancellationToken cancellationToken = default);
    Task UnlockExpenseDto(UnlockExpenseDto dto, CancellationToken cancellationToken = default);

    Task AddParticipantToExpenseAsync(AddExpenseParticipantDto dto, CancellationToken cancellationToken = default);

    Task RemoveParticipantFromExpenseAsync(RemoveExpenseParticipantDto dto,
        CancellationToken cancellationToken = default);

    Task AddItemToExpenseAsync(AddExpenseItemDto dto, CancellationToken cancellationToken = default);
    Task RemoveItemFromExpenseAsync(RemoveExpenseItemDto dto, CancellationToken cancellationToken = default);
    Task SelectItemInExpenseAsync(SelectExpenseItemDto dto, CancellationToken cancellationToken = default);
    Task UnselectItemInExpenseAsync(UnselectExpenseItemDto dto, CancellationToken cancellationToken = default);
}