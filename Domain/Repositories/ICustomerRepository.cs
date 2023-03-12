using Domain.Models;

namespace Domain.Repositories;

public interface ICustomerRepository
{
    Task<Customer> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Customer> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<Customer> GetByCredentialsAsync(string username, string password, CancellationToken cancellationToken = default);
    Task AddCustomerAsync(Customer customer, CancellationToken cancellationToken = default);
}