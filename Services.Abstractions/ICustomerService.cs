using Contracts.Authentication;
using Contracts.Responses;
using Contracts.Responses.Customers;

namespace Services.Abstractions;

public interface ICustomerService
{
    Task<CustomerResponse> GetCustomerByCredentialsAsync(SignInUserCredentials credentials, CancellationToken token = default);
    Task<CustomerResponse> CreateCustomerAsync(SignUpUserCredentials credentials, CancellationToken token = default);
    Task<CustomerResponse> GetCustomerByIdAsync(Guid customerId, CancellationToken token = default);
}