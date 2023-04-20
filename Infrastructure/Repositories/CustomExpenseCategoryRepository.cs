using Domain.Exceptions;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CustomExpenseCategoryRepository : ICustomExpenseCategoriesRepository
{
    private readonly AppDbContext _context;

    public CustomExpenseCategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddExpenseCategoryAsync(CustomExpenseCategory expenseCategory,
        CancellationToken cancellationToken = default)
    {
        var icon = await _context.Icons.FirstOrDefaultAsync(e => e.Id == expenseCategory.IconId, cancellationToken);
        if (icon is null)
        {
            throw new NotFoundException($"Icon with id {expenseCategory.IconId} not found");
        }

        if (icon.ExpenseCategoryId is not null)
        {
            throw new InvalidOperationException(
                $"Customer {expenseCategory.CustomerId} try to reassign icon {icon.Id} to new category");
        }
        icon.ExpenseCategoryId = expenseCategory.Id;
        _context.Icons.Update(icon);
        await _context.CustomExpenseCategories.AddAsync(expenseCategory, cancellationToken);
    }

    public async Task<CustomExpenseCategory> GetExpenseCategoryByIdAsync(Guid expenseCategoryId,
        CancellationToken cancellationToken = default)
    {
        var expenseCategory = await _context.CustomExpenseCategories
            .Include(e=>e.Icon)
            .FirstOrDefaultAsync(e => e.Id == expenseCategoryId, cancellationToken);
        if (expenseCategory is null)
        {
            throw new NotFoundException($"Expense category by id {expenseCategoryId} not found");
        }

        return expenseCategory;
    }

    public async Task<IEnumerable<CustomExpenseCategory>> GetAllCustomerExpenseCategories(Guid customerId,
        CancellationToken cancellationToken = default)
    {
        var result = await _context.CustomExpenseCategories
            .Where(e=>e.CustomerId==customerId)
            .Include(e=>e.Icon)
            .ToListAsync(cancellationToken);
        return result;
    }

    public async Task ChangeExpenseCategoryNameAsync(Guid userId, Guid expenseCategoryId, string name,
        CancellationToken cancellationToken = default)
    {
        var expenseCategory = await _context.CustomExpenseCategories
            .FirstOrDefaultAsync(e => e.Id == expenseCategoryId && e.CustomerId == userId, cancellationToken);
        if (expenseCategory is null)
        {
            throw new NotFoundException($"Expense category by id {expenseCategoryId} not found");
        }

        expenseCategory.Name = name;
        _context.CustomExpenseCategories.Update(expenseCategory);
    }
}