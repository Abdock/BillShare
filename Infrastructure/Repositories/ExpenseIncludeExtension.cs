using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BillShare.Extensions;

public static class ExpenseIncludeExtension
{
    public static IQueryable<Expense> IncludeEverything(this IQueryable<Expense> expense)
    {
        return expense
            .Include(e => e.Category)
            .Include(e => e.ExpenseType)
            .Include(e => e.ExpenseItems)
            .Include(e => e.ExpenseMultipliers)
            .Include(e => e.ExpenseParticipants)
            .ThenInclude(e => e.ExpenseParticipantItems)
            .Include(e => e.ExpenseParticipants)
            .ThenInclude(e => e.ExpenseParticipantItems)
            .ThenInclude(e => e.Item);
    }
}