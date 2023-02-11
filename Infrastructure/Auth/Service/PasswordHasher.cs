using System.Security.Cryptography;
using System.Text;
using Domain.Models;
using Infrastructure.Auth.Interfaces;
using Infrastructure.Database.Constants;
using Services.Extensions;

namespace Infrastructure.Auth.Service;

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