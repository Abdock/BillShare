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
        await _context.CustomExpenseCategories.AddAsync(expenseCategory, cancellationToken);
    }

    public async Task<CustomExpenseCategory> GetExpenseCategoryByIdAsync(Guid expenseCategoryId,
        CancellationToken cancellationToken = default)
    {
        var expenseCategory = await _context.CustomExpenseCategories
            .FirstOrDefaultAsync(e => e.Id == expenseCategoryId, cancellationToken);
        if (expenseCategory == null)
        {
            throw new NotFoundException($"Expense category by id {expenseCategoryId} not found");
        }

        return expenseCategory;
    }

    public async Task<IEnumerable<CustomExpenseCategory>> GetAllCustomerExpenseCategories(Guid customerId,
        CancellationToken cancellationToken = default)
    {
        return await _context.CustomExpenseCategories
            .Where(e=>e.CustomerId==customerId)
            .ToListAsync(cancellationToken);
    }

    public async Task ChangeExpenseCategoryNameAsync(Guid userId, Guid expenseCategoryId, string name,
        CancellationToken cancellationToken = default)
    {
        var expenseCategory = await _context.CustomExpenseCategories
            .FirstOrDefaultAsync(e => e.Id == expenseCategoryId && e.CustomerId == userId, cancellationToken);
        if (expenseCategory == null)
        {
            throw new NotFoundException($"Expense category by id {expenseCategoryId} not found");
        }

        expenseCategory.Name = name;
        _context.CustomExpenseCategories.Update(expenseCategory);
    }
}