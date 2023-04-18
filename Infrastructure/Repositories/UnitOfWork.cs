using Domain.Repositories;
using Infrastructure.Database.Context;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        CustomerRepository = new CustomerRepository(context);
        RefreshTokenRepository = new RefreshTokenRepository(context);
        FriendshipRepository = new FriendshipRepository(context);
        IconRepository = new IconRepository(context);
        CustomExpenseCategoriesRepository = new CustomExpenseCategoryRepository(context);
        ExpenseTypeRepository = new ExpenseTypeRepository(context);
        ExpenseRepository = new ExpenseRepository(context);
        AccountRepository = new AccountRepository(context);
        GroupRepository = new GroupRepository(context);
    }

    public ICustomerRepository CustomerRepository { get; }
    public IRefreshTokenRepository RefreshTokenRepository { get; }
    public IFriendshipRepository FriendshipRepository { get; }
    public IIconRepository IconRepository { get; }
    public ICustomExpenseCategoriesRepository CustomExpenseCategoriesRepository { get; }
    public IExpenseTypeRepository ExpenseTypeRepository { get; }
    public IExpenseRepository ExpenseRepository { get; }
    public IAccountRepository AccountRepository { get; }
    public IGroupRepository GroupRepository { get; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}