namespace Domain.Repositories;

public interface IUnitOfWork
{
    ICustomerRepository CustomerRepository { get; }
    IRefreshTokenRepository RefreshTokenRepository { get; }
    IFriendshipRepository FriendshipRepository { get; }
    IIconRepository IconRepository { get; }
    ICustomExpenseCategoriesRepository CustomExpenseCategoriesRepository { get; }
    IExpenseTypeRepository ExpenseTypeRepository { get; }
    IExpenseRepository ExpenseRepository { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}