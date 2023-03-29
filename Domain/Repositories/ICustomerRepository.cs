using Domain.Models;

namespace Domain.Repositories;

public interface ICustomerRepository
{
    Task<Customer> GetByCustomerIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Customer> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<IEnumerable<Customer>> GetCustomersWithSameUsername(string username, int skipCount, int takeCount,
        CancellationToken cancellationToken = default);

    Task<Customer> GetByCredentialsAsync(string username, string password,
        CancellationToken cancellationToken = default);

    Task AddCustomerAsync(Customer customer, CancellationToken cancellationToken = default);

    Task<IEnumerable<Customer>> GetFriendsAsync(Guid customerId, int skipCount, int takeCount,
        CancellationToken cancellationToken = default);

    Task<int> TotalFriendsCountAsync(Guid customerId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Customer>> GetIncomingFriendsAsync(Guid customerId, int skipCount, int takeCount,
        CancellationToken cancellationToken = default);

    Task<int> TotalIncomingFriendsCountAsync(Guid customerId, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Customer>> GetOutComingFriendsAsync(Guid customerId, int skipCount, int takeCount,
        CancellationToken cancellationToken = default);

    Task<int> TotalOutComingFriendsCountAsync(Guid customerId, CancellationToken cancellationToken = default);

    Task<int> TotalCountOfCustomersWithUsernameAsync(string username, CancellationToken cancellationToken = default);
}