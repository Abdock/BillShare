using Contracts.Authentication;

namespace Services.Abstractions.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticationToken> SignInAsync(SignInUserCredentials credentials,
        CancellationToken cancellationToken = default);

    Task<AuthenticationToken> SignUpAsync(SignUpUserCredentials credentials,
        CancellationToken cancellationToken = default);
}