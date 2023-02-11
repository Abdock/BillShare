namespace Contracts.Authentication;

public class AuthenticationToken
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
}