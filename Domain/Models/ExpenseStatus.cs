using Domain.Enums;

namespace Domain.Models;

public class ExpenseStatus
{
    public ExpenseStatusId Id { get; init; }
    public string Name { get; init; } = default!;
}