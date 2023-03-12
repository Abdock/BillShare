namespace Domain.Repositories;

public interface IUnitOfWork
{
    ICustomerRepository CustomerRepository { get; }
    IRefreshTokenRepository RefreshTokenRepository { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}