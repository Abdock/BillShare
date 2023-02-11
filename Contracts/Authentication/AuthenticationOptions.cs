namespace Contracts.Authentication;

public class AuthenticationOptions
{
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string Key { get; init; }
    public required TimeSpan AccessTokenLifetime { get; init; }
    public required TimeSpan RefreshTokenLifetime { get; init; }
}