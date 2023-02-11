using Contracts.Authentication;

namespace Services.Abstractions.Authentication;

public interface IJwtAuthenticationService
{
    Task<AuthenticationToken> SignInAsync(SignInUserCredentials credentials, CancellationToken token = default);

    Task<AuthenticationToken> SignUpAsync(SignUpUserCredentials credentials, CancellationToken token = default);
}