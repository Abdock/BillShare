using Domain.Enums;

namespace Domain.Models;

public class ExpenseItemStatus
{
    public ExpenseItemStatusId Id { get; init; }
    public string Name { get; init; } = default!;
}