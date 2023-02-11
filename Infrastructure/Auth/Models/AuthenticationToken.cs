namespace Infrastructure.Auth.Models;

public class AuthenticationToken
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
}