using Domain.Models;
using Domain.ValueObjects;

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
    
    Task<IEnumerable<Customer>> GetCustomersWithIdsAsync(IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default);

    void Update(Customer customer);

    Task<IReadOnlyCollection<Spending>> GetSpendsForPeriodAsync(Guid customerId, DateTime start, DateTime end,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<Spending>> GetSpendsSharedBetweenUsersAsync(Guid customerId1, Guid customerId2,
        CancellationToken cancellationToken = default);
}