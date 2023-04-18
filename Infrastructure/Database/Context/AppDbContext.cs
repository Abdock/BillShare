using Domain.Models;
using Infrastructure.Database.ModelsConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Context;

public class AppDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; } = default!;
    public DbSet<AccountStatus> AccountStatus { get; set; } = default!;
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<CustomExpenseCategory> CustomExpenseCategories { get; set; } = default!;
    public DbSet<Expense> Expenses { get; set; } = default!;
    public DbSet<ExpenseItem> ExpenseItems { get; set; } = default!;
    public DbSet<ExpenseItemStatus> ExpenseItemStatus { get; set; } = default!;
    public DbSet<ExpenseMultiplier> ExpenseMultipliers { get; set; } = default!;
    public DbSet<ExpenseParticipant> ExpenseParticipants { get; set; } = default!;
    public DbSet<ExpenseParticipantItem> ExpenseParticipantItems { get; set; } = default!;
    public DbSet<ExpenseParticipantItemStatus> ExpenseParticipantItemStatus { get; set; } = default!;
    public DbSet<ExpenseParticipantStatus> ExpenseParticipantStatus { get; set; } = default!;
    public DbSet<ExpenseStatus> ExpenseStatus { get; set; } = default!;
    public DbSet<ExpenseType> ExpenseTypes { get; set; } = default!;
    public DbSet<Friendship> Friendships { get; set; } = default!;
    public DbSet<FriendshipStatus> FriendshipStatus { get; set; } = default!;
    public DbSet<Icon> Icons { get; set; } = default!;
    public DbSet<Password> Passwords { get; set; } = default!;
    public DbSet<Role> Roles { get; set; } = default!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;
    public DbSet<Group> Groups { get; set; } = default!;

    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new AccountStatusConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new CustomExpenseCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseItemConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseItemStatusConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseMultiplierConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseParticipantConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseParticipantItemConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseParticipantItemStatusConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseParticipantStatusConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseStatusConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseTypeConfiguration());
        modelBuilder.ApplyConfiguration(new FriendshipConfiguration());
        modelBuilder.ApplyConfiguration(new FriendshipStatusConfiguration());
        modelBuilder.ApplyConfiguration(new IconConfiguration());
        modelBuilder.ApplyConfiguration(new PasswordConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new GroupsConfiguration());
    }
}