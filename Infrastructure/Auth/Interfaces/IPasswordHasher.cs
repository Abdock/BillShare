using Domain.Models;

namespace Infrastructure.Auth.Interfaces;

public interface IPasswordHasher
{
    Password HashPassword(Customer customer, string password);
}