using AutoMapper;
using Contracts.DTOs.Accounts;
using Contracts.Responses.Accounts;
using Contracts.Responses.Expenses;
using Domain.Models;
using Domain.Repositories;
using Services.Abstractions;

namespace Services;

public class AccountService : IAccountService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AccountResponse> AddAccountAsync(AddAccountDto dto, CancellationToken cancellationToken = default)
    {
        var account = _mapper.Map<Account>(dto);
        await _unitOfWork.AccountRepository.AddAccountAsync(account, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<AccountResponse>(account);
    }

    public async Task<IEnumerable<AccountResponse>> GetUserAccountsAsync(GetUserAccountsDto dto,
        CancellationToken cancellationToken = default)
    {
        var accounts = await _unitOfWork.AccountRepository.GetAccountsAsync(dto.UserId, cancellationToken);
        return accounts.Select(account => _mapper.Map<AccountResponse>(account));
    }

    public async Task<AccountResponse> GetAccountAsync(GetUserAccountDto dto,
        CancellationToken cancellationToken = default)
    {
        var account = await _unitOfWork.AccountRepository.GetAccountAsync(dto.UserId, dto.AccountId, cancellationToken);
        return _mapper.Map<AccountResponse>(account);
    }

    public async Task DisableAccountAsync(DisableAccountDto dto, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.AccountRepository.DisableAccountAsync(dto.UserId, dto.AccountId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task EnableAccountAsync(EnableAccountDto dto, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.AccountRepository.EnableAccountAsync(dto.UserId, dto.AccountId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task ChangeAccountAmountAsync(ChangeAccountAmountDto dto,
        CancellationToken cancellationToken = default)
    {
        await _unitOfWork.AccountRepository.ChangeAmountAsync(dto.UserId, dto.AccountId, dto.Amount, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<ShortExpenseResponse>> GetPaidExpensesByAccountAsync(GetPaidExpensesDto dto,
        CancellationToken cancellationToken = default)
    {
        var expenses = await _unitOfWork.AccountRepository
            .GetPaidExpensesByAccountAsync(dto.UserId, dto.AccountId, cancellationToken);
        return expenses.Select(expense => _mapper.Map<ShortExpenseResponse>(expense));
    }
}