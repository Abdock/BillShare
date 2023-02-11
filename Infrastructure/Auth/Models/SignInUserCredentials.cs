namespace Infrastructure.Auth.Models;

public class SignInUserCredentials
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}