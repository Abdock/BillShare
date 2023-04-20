using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Contracts.Attributes;

public class PasswordValidationAttribute : ValidationAttribute
{
    private const string PasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$";

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not null)
        {
            var password = value.ToString()!;

            if (!Regex.IsMatch(password, PasswordRegex))
            {
                return new ValidationResult(ErrorMessage);
            }
        }

        return ValidationResult.Success;
    }
}