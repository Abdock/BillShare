using Domain.Models;

namespace Domain.Repositories;

public interface ICustomerRepository
{
    Task<Customer> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Customer> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<Customer> GetByCredentialsAsync(string username, string password,
        CancellationToken cancellationToken = default);

    Task AddCustomerAsync(Customer customer, CancellationToken cancellationToken = default);

    Task<IEnumerable<Customer>> GetFriendsAsync(Guid customerId, int skipCount, int takeCount,
        CancellationToken cancellationToken = default);

    Task<int> TotalFriendsCountAsync(Guid customerId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Customer>> GetIncomingFriendsAsync(Guid customerId, int skipCount, int takeCount,
        CancellationToken cancellationToken = default);

    Task<int> TotalIncomingFriendsCountAsync(Guid customerId, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Customer>> GetOutcomingFriendsAsync(Guid customerId, int skipCount, int takeCount,
        CancellationToken cancellationToken = default);

    Task<int> TotalOutcomingFriendsCountAsync(Guid customerId, CancellationToken cancellationToken = default);
}