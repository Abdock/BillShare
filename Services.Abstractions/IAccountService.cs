using Contracts.DTOs.Accounts;
using Contracts.Responses.Accounts;
using Contracts.Responses.Expenses;

namespace Services.Abstractions;

public interface IAccountService
{
    Task<AccountResponse> AddAccountAsync(AddAccountDto dto, CancellationToken cancellationToken = default);

    Task<IEnumerable<AccountResponse>> GetUserAccountsAsync(GetUserAccountsDto dto,
        CancellationToken cancellationToken = default);

    Task<AccountResponse> GetAccountAsync(GetUserAccountDto dto, CancellationToken cancellationToken = default);

    Task DisableAccountAsync(DisableAccountDto dto, CancellationToken cancellationToken = default);
    Task EnableAccountAsync(EnableAccountDto dto, CancellationToken cancellationToken = default);
    Task ChangeAccountAmountAsync(ChangeAccountAmountDto dto, CancellationToken cancellationToken = default);

    Task<IEnumerable<ShortExpenseResponse>> GetPaidExpensesByAccountAsync(GetPaidExpensesDto dto,
        CancellationToken cancellationToken = default);
}