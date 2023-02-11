using Domain.Models;

namespace Services.Abstractions.Authentication;

public interface IPasswordHasher
{
    Password HashPassword(Customer customer, string password);
}