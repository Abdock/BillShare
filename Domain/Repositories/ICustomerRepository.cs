using Domain.Models;

namespace Domain.Repositories;

public interface ICustomerRepository
{
    Task<Customer> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<Customer> GetByEmailAsync(string email, CancellationToken token = default);
    Task<Customer> GetByCredentialsAsync(string username, string password, CancellationToken token = default);
    Task AddCustomerAsync(Customer customer, CancellationToken token = default);
}