namespace Domain.Repositories;

public interface IUnitOfWork
{
    ICustomerRepository CustomerRepository { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}