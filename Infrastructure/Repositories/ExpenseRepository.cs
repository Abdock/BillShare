using BillShare.Extensions;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Infrastructure.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly AppDbContext _context;

    public ExpenseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task LoadRelatedDataAsync(Expense expense, CancellationToken cancellationToken = default)
    {
        foreach (var reference in _context.Entry(expense).References)
        {
            await reference.LoadAsync(cancellationToken);
        }

        foreach (var collection in _context.Entry(expense).Collections)
        {
            await collection.LoadAsync(cancellationToken);
        }
    }

    public async Task AddExpenseAsync(Expense expense, CancellationToken cancellationToken = default)
    {
        var participants = expense.ExpenseParticipants;
        var items = expense.ExpenseItems;
        var participantItems = participants
            .SelectMany(e => items, (participant, item) => new ExpenseParticipantItem
            {
                ExpenseParticipantId = participant.Id,
                ItemId = item.Id,
                StatusId = ExpenseParticipantItemStatusId.Unselected
            }).ToList();
        Console.WriteLine(participantItems);

        await _context.Expenses.AddAsync(expense, cancellationToken);
        await _context.ExpenseParticipantItems.AddRangeAsync(participantItems, cancellationToken);
    }

    public async Task<Expense> GetExpenseByIdAsync(Guid expenseId, Guid customerId,
        CancellationToken cancellationToken = default)
    {
        var expense = await _context.Expenses.Where(e => e.Id == expenseId)
            .IncludeEverything()
            .Where(e => e.ExpenseParticipants.Any(e => e.CustomerId == customerId))
            .FirstOrDefaultAsync(cancellationToken);
        if (expense is null)
        {
            throw new NotFoundException($"Expense with id {expenseId} in participant id {customerId} not found");
        }

        return expense;
    }

    public async Task<IEnumerable<Expense>> GetPagedExpensesAsync(Guid customerId, int skipCount, int takeCount,
        CancellationToken cancellationToken = default)
    {
        return _context.Expenses
            .IncludeEverything()
            .Where(delegate(Expense e)
            {
                return e.ExpenseParticipants.Any(e => e.CustomerId == customerId);
            })
            .Skip(skipCount)
            .Take(takeCount)
            .ToList();
        return await _context.ExpenseParticipants
        .Where(e => e.CustomerId == customerId)
    .Skip(skipCount)
        .Take(takeCount)
        .Include(e => e.Expense)
        .Select(e => e.Expense)
        .ToListAsync(cancellationToken);
}

public async Task LockExpenseAsync(Guid customerId, Guid expenseId, CancellationToken cancellationToken = default)
{
    var expense = await _context.ExpenseParticipants
        .Where(e => e.CustomerId == customerId && e.ExpenseId == expenseId)
        .Include(e => e.Expense)
        .Select(e => e.Expense)
        .FirstOrDefaultAsync(cancellationToken);
    if (expense is null)
    {
        throw new NotFoundException($"Expense with id {expenseId} in participant id {customerId} not found");
    }

    expense.StatusId = ExpenseStatusId.Finished;
    _context.Expenses.Update(expense);
}

public async Task UnlockExpenseAsync(Guid customerId, Guid expenseId, CancellationToken cancellationToken = default)
{
    var expense = await _context.ExpenseParticipants
        .Where(e => e.CustomerId == customerId && e.ExpenseId == expenseId)
        .Include(e => e.Expense)
        .Select(e => e.Expense)
        .FirstOrDefaultAsync(cancellationToken);
    if (expense is null)
    {
        throw new NotFoundException($"Expense with id {expenseId} in participant id {customerId} not found");
    }

    expense.StatusId = ExpenseStatusId.Active;
    _context.Expenses.Update(expense);
}

public async Task<int> TotalCountAsync(Guid customerId, CancellationToken cancellationToken = default)
{
    return await _context.ExpenseParticipants
        .Where(e => e.CustomerId == customerId)
        .CountAsync(cancellationToken);
}

public async Task AddParticipantAsync(Guid expenseId, Guid customerId,
    CancellationToken cancellationToken = default)
{
    var isCustomerExists = await _context.Customers.AnyAsync(e => e.Id == customerId, cancellationToken);
    if (isCustomerExists)
    {
        throw new NotFoundException($"Customer with id {customerId} not found");
    }

    var isExpenseExists = await _context.Expenses.AnyAsync(e => e.Id == expenseId, cancellationToken);
    if (isExpenseExists)
    {
        throw new NotFoundException($"Expense with id {expenseId} not found");
    }

    var participant = new ExpenseParticipant
    {
        ExpenseId = expenseId,
        CustomerId = customerId,
        StatusId = ExpenseParticipantStatusId.Unpaid
    };
    await _context.ExpenseParticipants.AddAsync(participant, cancellationToken);
}

public async Task DeleteParticipantAsync(Guid expenseId, Guid participantId,
    CancellationToken cancellationToken = default)
{
    var participant = await _context.ExpenseParticipants.FirstOrDefaultAsync(
        e => e.Id == participantId && e.ExpenseId == expenseId,
        cancellationToken);
    if (participant is null)
    {
        throw new NotFoundException(
            $"Expense participant with id {participantId} in expense {expenseId} not found");
    }

    _context.ExpenseParticipants.Remove(participant);
}

public async Task SelectItemAsync(Guid expenseId, Guid expenseItemId, Guid customerId,
    CancellationToken cancellationToken = default)
{
    var expense = await _context.Expenses
        .IncludeEverything()
        .Where(e => e.Id == expenseId)
        .FirstOrDefaultAsync(cancellationToken);
    if (expense is null)
    {
        throw new NotFoundException($"Expense with id {expenseId} not found");
    }

    var participant = expense.ExpenseParticipants.FirstOrDefault(e => e.CustomerId == customerId);
    if (participant is null)
    {
        throw new NotFoundException($"Expense with id {expenseId} not contain customer with id {customerId}");
    }

    var item = _context.ExpenseParticipantItems.FirstOrDefault(e =>
        e.ItemId == expenseItemId && e.ExpenseParticipantId == participant.Id);
    if (item is null)
    {
        throw new NotFoundException($"Expense with id {expenseId} not contain item with id {expenseItemId}");
    }

    item.StatusId = ExpenseParticipantItemStatusId.Selected;
    _context.ExpenseParticipantItems.Update(item);
}

public async Task UnselectItemAsync(Guid expenseId, Guid expenseItemId, Guid customerId,
    CancellationToken cancellationToken = default)
{
    var expense = await _context.Expenses
        .IncludeEverything()
        .Where(e => e.Id == expenseId)
        .FirstOrDefaultAsync(cancellationToken);
    if (expense is null)
    {
        throw new NotFoundException($"Expense with id {expenseId} not found");
    }

    var participant = expense.ExpenseParticipants.FirstOrDefault(e => e.CustomerId == customerId);
    if (participant is null)
    {
        throw new NotFoundException($"Expense with id {expenseId} not contain customer with id {customerId}");
    }

    var item = _context.ExpenseParticipantItems.FirstOrDefault(e =>
        e.ItemId == expenseItemId && e.ExpenseParticipantId == participant.Id);
    if (item is null)
    {
        throw new NotFoundException($"Expense with id {expenseId} not contain item with id {expenseItemId}");
    }

    item.StatusId = ExpenseParticipantItemStatusId.Unselected;
    _context.ExpenseParticipantItems.Update(item);
}

public async Task DeleteItemAsync(Guid expenseId, Guid expenseItemId, Guid customerId,
    CancellationToken cancellationToken = default)
{
    var expense = await _context.Expenses
        .Where(e => e.Id == expenseId)
        .Include(e => e.ExpenseParticipants)
        .Include(e => e.ExpenseItems)
        .FirstOrDefaultAsync(cancellationToken);
    if (expense is null)
    {
        throw new NotFoundException($"Expense with id {expenseId} not found");
    }

    var isCustomerIsParticipantOfExpense = expense.ExpenseParticipants.Any(e => e.CustomerId == customerId);
    if (isCustomerIsParticipantOfExpense)
    {
        throw new NotFoundException(
            $"Customer with id {customerId} not participant in expense with id {expenseId}");
    }

    var item = expense.ExpenseItems.FirstOrDefault(e => e.Id == expenseItemId);
    if (item is not null)
    {
        expense.ExpenseItems.Remove(item);
        // TODO write algorithm to remove participant items
    }
}

public async Task AddItemAsync(Guid expenseId, Guid customerId, ExpenseItem expenseItem,
    CancellationToken cancellationToken = default)
{
    var expense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == expenseId, cancellationToken);
    if (expense is null)
    {
        throw new NotFoundException($"Expense with id {expenseId} not found");
    }
}

}