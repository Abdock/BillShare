using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Auth.Models;

public class AuthenticationOptions
{
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string Key { get; init; }
    public required TimeSpan AccessTokenLifetime { get; init; }
    public required TimeSpan RefreshTokenLifetime { get; init; }

    public SecurityKey GenerateSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}