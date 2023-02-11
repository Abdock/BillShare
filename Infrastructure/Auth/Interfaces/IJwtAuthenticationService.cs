using Domain.Models;
using Infrastructure.Auth.Models;

namespace Infrastructure.Auth.Interfaces;

public interface IJwtAuthenticationService
{
    Task<AuthenticationToken> SignInAsync(SignInUserCredentials credentials, CancellationToken token = default);

    Task<AuthenticationToken> SignUpAsync(SignUpUserCredentials credentials, CancellationToken token = default);
}