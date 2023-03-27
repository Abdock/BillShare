using Domain.Enums;
using Domain.Exceptions;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly AppDbContext _context;

    public ExpenseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddExpenseAsync(Expense expense, CancellationToken cancellationToken = default)
    {
        await _context.Expenses.AddAsync(expense, cancellationToken);
    }

    public async Task<Expense> GetExpenseByIdAsync(Guid expenseId, Guid customerId, CancellationToken cancellationToken = default)
    {
        var expense = await _context.ExpenseParticipants
            .Where(e => e.CustomerId == customerId && e.ExpenseId == expenseId)
            .Include(e => e.Expense)
            .Select(e => e.Expense)
            .FirstOrDefaultAsync(cancellationToken);
        if (expense == null)
        {
            throw new NotFoundException($"Expense with id {expenseId} in participant id {customerId} not found");
        }

        return expense;
    }

    public async Task<IEnumerable<Expense>> GetPagedExpensesAsync(Guid customerId, int skipCount, int takeCount,
        CancellationToken cancellationToken = default)
    {
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
            .Select(e=>e.Expense)
            .FirstOrDefaultAsync(cancellationToken);
        if (expense == null)
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
            .Select(e=>e.Expense)
            .FirstOrDefaultAsync(cancellationToken);
        if (expense == null)
        {
            throw new NotFoundException($"Expense with id {expenseId} in participant id {customerId} not found");
        }

        expense.StatusId = ExpenseStatusId.Active;
        _context.Expenses.Update(expense);
    }
}