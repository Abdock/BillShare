using Contracts.Authentication;
using Services.Abstractions;
using Services.Abstractions.Authentication;

namespace Infrastructure.Authentication.Service;
public class AuthenticationService : IAuthenticationService
{
    private readonly ICustomerService _customerService;
    private readonly ITokenGeneratorService _tokenGeneratorService;

    public AuthenticationService(ICustomerService customerService, ITokenGeneratorService tokenGeneratorService)
    {
        _customerService = customerService;
        _tokenGeneratorService = tokenGeneratorService;
    }

    public async Task<AuthenticationToken> SignInAsync(SignInUserCredentials credentials,
        CancellationToken cancellationToken = default)
    {
        var customer = await _customerService.GetCustomerByCredentialsAsync(credentials, cancellationToken);
        return await _tokenGeneratorService.GenerateJwtTokenAsync(customer, cancellationToken);
    }

    public async Task<AuthenticationToken> SignUpAsync(SignUpUserCredentials credentials,
        CancellationToken cancellationToken = default)
    {
        var customer = await _customerService.CreateCustomerAsync(credentials, cancellationToken);
        return await _tokenGeneratorService.GenerateJwtTokenAsync(customer, cancellationToken);
    }
}