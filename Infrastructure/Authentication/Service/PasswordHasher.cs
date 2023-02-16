using System.Security.Cryptography;
using System.Text;
using Domain.Models;
using Infrastructure.Database.Constants;
using Services.Abstractions.Authentication;
using Services.Extensions;

namespace Infrastructure.Authentication.Service;

public class PasswordHasher : IPasswordHasher
{
    private static string GenerateSalt(int length)
    {
        var saltBytes = RandomNumberGenerator.GetBytes(length / 2);
        var saltBuilder = new StringBuilder();
        foreach (var saltByte in saltBytes)
        {
            saltBuilder.Append(saltByte.ToString("x2"));
        }

        return saltBuilder.ToString();
    }

    public Password HashPassword(Customer customer, string password)
    {
        var salt = GenerateSalt(ModelsConstants.SaltMaxLength);
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