using System.Runtime.CompilerServices;
using Domain.Base;
using Domain.Enums;

[assembly: InternalsVisibleTo("Infrastructure")]
namespace Domain.Models;

public class Customer : BaseEntity
{
    public string? ExternalId { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? TelegramHandle { get; set; } = default!;
    public string? PhoneNumber { get; set; } = default!;
    public string Name { get; init; } = default!;
    public string? AvatarUrl { get; set; } = default!;

    public Guid PasswordId { get; set; }

    public RoleId RoleId { get; set; }
    
    public Role Role { get; set; } = default!;
    public Password Password { get; set; } = default!;
    public IEnumerable<Friendship> Friendships => FriendFriendships.Concat(UserFriendships).Distinct();
    public ICollection<Account> Accounts { get; init; } = new HashSet<Account>();
    public ICollection<CustomExpenseCategory> ExpenseCategories { get; init; } = new HashSet<CustomExpenseCategory>();
    public ICollection<Expense> CreatedExpenses { get; init; } = new HashSet<Expense>();

    public IEnumerable<Customer> Friends => Friendships
        .Where(friendship => friendship.UserId == Id)
        .Select(friendship => friendship.Friend);

    internal ICollection<Friendship> FriendFriendships { get; init; } = new HashSet<Friendship>();
    internal ICollection<Friendship> UserFriendships { get; init; } = new HashSet<Friendship>();

    public ICollection<Group> CreatedGroups { get; init; } = new HashSet<Group>();
    public ICollection<Group> Groups { get; init; } = new HashSet<Group>();
}