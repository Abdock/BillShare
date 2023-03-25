using Domain.Models;

namespace Domain.Repositories;

public interface IExpenseTypeRepository
{
    Task<IEnumerable<ExpenseType>> GetAllExpenseTypesAsync(CancellationToken cancellationToken = default);
}