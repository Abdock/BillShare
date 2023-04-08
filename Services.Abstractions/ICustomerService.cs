using Contracts.Authentication;
using Contracts.DTOs.Customers;
using Contracts.DTOs.Icons;
using Contracts.Responses.Customers;

namespace Services.Abstractions;

public interface ICustomerService
{
    Task<CustomerResponse> GetCustomerByCredentialsAsync(SignInUserCredentials credentials,
        CancellationToken cancellationToken = default);

    Task<CustomerResponse> CreateCustomerAsync(SignUpUserCredentials credentials,
        CancellationToken cancellationToken = default);

    Task<CustomerResponse> GetCustomerByIdAsync(Guid customerId, CancellationToken cancellationToken = default);

    Task<CustomerAvatarIcon> ChangeCustomerAvatarAsync(ChangeCustomerAvatarDto dto,
        CancellationToken cancellationToken = default);

    Task DeleteCustomerAvatarAsync(Guid customerId, CancellationToken cancellationToken = default);
}