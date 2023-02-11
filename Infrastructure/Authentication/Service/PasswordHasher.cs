using System.Security.Cryptography;
using System.Text;
using Domain.Models;
using Infrastructure.Database.Constants;
using Services.Abstractions.Authentication;
using Services.Extensions;

namespace Infrastructure.Authentication.Service;

public class PasswordHasher : IPasswordHasher
{
    public Password HashPassword(Customer customer, string password)
    {
        var saltBytes = RandomNumberGenerator.GetBytes(ModelsConstants.SaltMaxLength);
        var salt = Encoding.UTF8.GetString(saltBytes);
        var hashedPassword = (salt + password).ComputeSha256();
        var encryptionResult = new Password
        {
            Salt = salt,
            Customer = customer,
            EncryptedPassword = hashedPassword
        };
        return encryptionResult;
    }
}