using Domain.Enums;

namespace Domain.Models;

public class ExpenseType
{
    public ExpenseTypeId Id { get; init; }
    public string Name { get; init; } = default!;
}