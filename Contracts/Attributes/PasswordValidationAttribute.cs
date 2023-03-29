using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Contracts.Attributes;

public class PasswordValidationAttribute : ValidationAttribute
{
    private readonly string passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$";

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            var password = value.ToString()!;

            if (!Regex.IsMatch(password, passwordRegex))
            {
                return new ValidationResult(ErrorMessage);
            }
        }

        return ValidationResult.Success;
    }
}