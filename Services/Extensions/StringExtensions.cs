using System.Security.Cryptography;
using System.Text;

namespace Services.Extensions;

public static class StringExtensions
{
    public static string ComputeSha256(this string value)
    {
        var data = Encoding.UTF8.GetBytes(value);
        var hashedData = SHA256.HashData(data);
        var hashBuilder = new StringBuilder();
        foreach (var symbol in hashedData)
        {
            hashBuilder.Append(symbol.ToString("x2"));
        }

        var hashedValue = hashBuilder.ToString();
        return hashedValue;
    }
}