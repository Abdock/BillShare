using Domain.Models;

namespace Domain.Repositories;

public interface IAccountRepository
{
    Task AddAccountAsync(Account account, CancellationToken cancellationToken = default);
    Task<IEnumerable<Account>> GetAccountsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Account> GetAccountAsync(Guid userId, Guid accountId, CancellationToken cancellationToken = default);
    Task DisableAccountAsync(Guid userId, Guid accountId, CancellationToken cancellationToken = default);
    Task EnableAccountAsync(Guid userId, Guid accountId, CancellationToken cancellationToken = default);
    Task ChangeAmountAsync(Guid userId, Guid accountId, decimal amount, CancellationToken cancellationToken = default);

    Task<IEnumerable<Expense>> GetPaidExpensesByAccountAsync(Guid userId, Guid accountId,
        CancellationToken cancellationToken = default);
}