using System.Text;
using Contracts.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication.Extensions;

public static class AuthenticationOptionsExtensions
{
    public static SecurityKey GenerateSecurityKey(this AuthenticationOptions options)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));
    }
}