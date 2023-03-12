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

    public async Task<Customer> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var customer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (customer == null)
        {
            throw new NotFoundException($"Customer with id {id} not found");
        }
        
        return customer;
    }

    public async Task<Customer> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var customer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(e => e.Email == email, cancellationToken);
        if (customer == null)
        {
            throw new NotFoundException($"Customer with email {email} not found");
        }

        return customer;
    }

    public async Task<Customer> GetByCredentialsAsync(string username, string password,
        CancellationToken cancellationToken = default)
    {
        var customer = await _context.Customers
            .Include(e => e.Password)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Name == username, cancellationToken);
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

    public async Task AddCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(customer, cancellationToken);
    }
}