namespace Domain.Repositories;

public interface IUnitOfWork
{
    ICustomerRepository CustomerRepository { get; }
    IRefreshTokenRepository RefreshTokenRepository { get; }
    IFriendshipRepository FriendshipRepository { get; }
    IIconRepository IconRepository { get; }
    ICustomExpenseCategoriesRepository CustomExpenseCategoriesRepository { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}