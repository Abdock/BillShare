using Domain.Exceptions;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using Services.Extensions;

namespace Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Customer> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        var customer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, token);
        if (customer == null)
        {
            throw new NotFoundException($"Customer with id {id} not found");
        }
        
        return customer;
    }

    public async Task<Customer> GetByEmailAsync(string email, CancellationToken token = default)
    {
        var customer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(e => e.Email == email, token);
        if (customer == null)
        {
            throw new NotFoundException($"Customer with email {email} not found");
        }

        return customer;
    }

    public async Task<Customer> GetByCredentialsAsync(string username, string password,
        CancellationToken token = default)
    {
        var customer = await _context.Customers
            .Include(e => e.Password)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Name == username, token);
        if (customer == null)
        {
            throw new NotFoundException("Invalid user credentials");
        }

        var hashedPassword = (customer.Password.Salt + password).ComputeSha256();
        if (hashedPassword != customer.Password.EncryptedPassword)
        {
            throw new NotFoundException("Invalid user credentials");
        }

        return customer;
    }

    public async Task AddCustomerAsync(Customer customer, CancellationToken token = default)
    {
        await _context.AddAsync(customer, token);
    }
}