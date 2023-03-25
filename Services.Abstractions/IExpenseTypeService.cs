using Contracts.Responses.ExpenseTypes;

namespace Services.Abstractions;

public interface IExpenseTypeService
{
    Task<IEnumerable<ExpenseTypeResponse>> GetAllExpenseTypesAsync(CancellationToken cancellationToken = default);
}