using Contracts.DTOs.Customers;
using Contracts.Responses.Customers;
using Contracts.Responses.General;

namespace Services.Abstractions;

public interface IUserService
{
    Task<CustomerResponse> GetInformationAboutCustomerAsync(Guid customerId,
        CancellationToken cancellationToken = default);

    Task<PagedResponse<RelatedCustomerResponse>> SearchCustomersWithUsername(SearchCustomersByUsernameDto dto,
        CancellationToken cancellationToken = default);
}