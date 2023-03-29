using Domain.Enums;
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

    public async Task<Customer> GetByCustomerIdAsync(Guid id, CancellationToken cancellationToken = default)
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

    public async Task<IEnumerable<Customer>> GetCustomersWithSameUsername(string username, int skipCount, int takeCount,
        CancellationToken cancellationToken = default)
    {
        return await _context.Customers
            .Where(e => e.Name.ToLower().Contains(username.ToLower()))
            .Skip(skipCount)
            .Take(takeCount)
            .Include(e=>e.UserFriendships)
            .Include(e=>e.FriendFriendships)
            .ToListAsync(cancellationToken);
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

    public async Task<IEnumerable<Customer>> GetFriendsAsync(Guid customerId, int skipCount, int takeCount,
        CancellationToken cancellationToken = default)
    {
        return await _context.Friendships
            .Where(e => e.UserId == customerId && e.StatusId == FriendshipStatusId.Accepted)
            .Skip(skipCount)
            .Take(takeCount)
            .Include(e => e.Friend)
            .Select(e => e.Friend)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> TotalFriendsCountAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return await _context.Friendships
            .Where(e => e.UserId == customerId && e.StatusId == FriendshipStatusId.Accepted)
            .CountAsync(cancellationToken);
    }

    public async Task<IEnumerable<Customer>> GetIncomingFriendsAsync(Guid customerId, int skipCount, int takeCount, CancellationToken cancellationToken = default)
    {
        return await _context.Friendships
            .Where(e => e.FriendId == customerId && e.StatusId == FriendshipStatusId.Pending)
            .Skip(skipCount)
            .Take(takeCount)
            .Include(e => e.User)
            .Select(e => e.User)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> TotalIncomingFriendsCountAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return await _context.Friendships
            .Where(e => e.FriendId == customerId && e.StatusId == FriendshipStatusId.Pending)
            .CountAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<Customer>> GetOutcomingFriendsAsync(Guid customerId, int skipCount, int takeCount, CancellationToken cancellationToken = default)
    {
        return await _context.Friendships
            .Where(e => e.UserId == customerId && e.StatusId == FriendshipStatusId.Pending)
            .Skip(skipCount)
            .Take(takeCount)
            .Include(e => e.Friend)
            .Select(e => e.Friend)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> TotalOutcomingFriendsCountAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return await _context.Friendships
            .Where(e => e.UserId == customerId && e.StatusId == FriendshipStatusId.Pending)
            .CountAsync(cancellationToken);
    }

    public async Task<int> TotalCountOfCustomersWithUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _context.Customers
            .Where(e => e.Name.ToLower().Contains(username.ToLower()))
            .CountAsync(cancellationToken);
    }
}