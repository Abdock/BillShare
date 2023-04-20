using Domain.Enums;
using Domain.Exceptions;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;

    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAccountAsync(Account account, CancellationToken cancellationToken = default)
    {
        await _context.Accounts.AddAsync(account, cancellationToken);
    }

    public async Task<IEnumerable<Account>> GetAccountsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Accounts
            .Where(e => e.UserId == userId && e.StatusId == AccountStatusId.Active)
            .ToListAsync(cancellationToken);
    }

    public async Task<Account> GetAccountAsync(Guid userId, Guid accountId, CancellationToken cancellationToken = default)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(e => e.Id == accountId && e.UserId == userId, cancellationToken);
        if (account is null)
        {
            throw new NotFoundException($"Account with id {accountId} in user {userId}");
        }

        return account;
    }

    public async Task DisableAccountAsync(Guid userId, Guid accountId, CancellationToken cancellationToken = default)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(e => e.Id == accountId && e.UserId == userId, cancellationToken);
        if (account is null)
        {
            throw new NotFoundException($"Account with id {accountId} in user {userId}");
        }

        account.StatusId = AccountStatusId.NotActive;
        _context.Accounts.Update(account);
    }

    public async Task EnableAccountAsync(Guid userId, Guid accountId, CancellationToken cancellationToken = default)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(e => e.Id == accountId && e.UserId == userId, cancellationToken);
        if (account is null)
        {
            throw new NotFoundException($"Account with id {accountId} in user {userId}");
        }

        account.StatusId = AccountStatusId.Active;
        _context.Accounts.Update(account);
    }

    public async Task ChangeAmountAsync(Guid userId, Guid accountId, decimal amount, CancellationToken cancellationToken = default)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(e => e.Id == accountId && e.UserId == userId, cancellationToken);
        if (account is null)
        {
            throw new NotFoundException($"Account with id {accountId} in user {userId}");
        }

        account.Amount = amount;
        _context.Accounts.Update(account);
    }

    public async Task<IEnumerable<Expense>> GetPaidExpensesByAccountAsync(Guid userId, Guid accountId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Accounts
            .Where(e => e.Id == accountId && e.UserId == userId)
            .Include(e => e.Expenses)
            .SelectMany(e => e.Expenses)
            .Where(e => e.StatusId == ExpenseStatusId.Finished)
            .ToListAsync(cancellationToken);
    }
}