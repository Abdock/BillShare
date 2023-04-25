using Domain.Base;
using Domain.Enums;

namespace Domain.Models;

public class Expense : BaseEntity
{
    public Guid CreatorId { get; init; }
    public Customer Creator { get; init; } = default!;
    public string Name { get; init; } = default!;
    public ExpenseTypeId ExpenseTypeId { get; init; }
    public ExpenseType ExpenseType { get; init; } = default!;
    public Guid CategoryId { get; init; }
    public CustomExpenseCategory Category { get; init; } = default!;
    public Guid AccountId { get; init; }
    public Account Account { get; init; } = default!;
    public decimal Amount { get; init; }
    public DateTime DateTime { get; init; }
    public ExpenseStatusId StatusId { get; set; }
    public ExpenseStatus Status { get; set; } = default!;
    public ICollection<ExpenseItem> ExpenseItems { get; set; } = new HashSet<ExpenseItem>();
    public ICollection<ExpenseParticipant> ExpenseParticipants { get; set; } = new HashSet<ExpenseParticipant>();
    public ICollection<ExpenseMultiplier> ExpenseMultipliers { get; init; } = new HashSet<ExpenseMultiplier>();
}