using Domain.Enums;
using Domain.Exceptions;
using Domain.Models;
using Domain.Repositories;
using Domain.ValueObjects;
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
        if (customer is null)
        {
            throw new NotFoundException($"Customer with id {id} not found");
        }

        return customer;
    }

    public async Task<Customer> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var customer = await _context.Customers.AsNoTracking()
            .FirstOrDefaultAsync(e => e.Email == email, cancellationToken);
        if (customer is null)
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
            .Include(e => e.UserFriendships)
            .ThenInclude(e => e.Friend)
            .Include(e => e.FriendFriendships)
            .ThenInclude(e => e.User)
            .ToListAsync(cancellationToken);
    }

    public async Task<Customer> GetByCredentialsAsync(string username, string password,
        CancellationToken cancellationToken = default)
    {
        var customer = await _context.Customers
            .Include(e => e.Password)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Name == username, cancellationToken);
        if (customer is null)
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
        var isUsernameUsed = await _context.Customers.AnyAsync(e => e.Name == customer.Name, cancellationToken);
        if (isUsernameUsed)
        {
            throw new UserAlreadyExistsException("Customer username already used");
        }

        var isEmailUsed = await _context.Customers.AnyAsync(e => e.Email == customer.Email, cancellationToken);
        if (isEmailUsed)
        {
            throw new UserAlreadyExistsException("Customer email already used");
        }

        var account = new Account
        {
            Amount = 0,
            UserId = customer.Id,
            Name = "Cash",
            ExternalId = null,
            StatusId = AccountStatusId.Active
        };
        customer.Accounts.Add(account);
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

    public async Task<IEnumerable<Customer>> GetIncomingFriendsAsync(Guid customerId, int skipCount, int takeCount,
        CancellationToken cancellationToken = default)
    {
        return await _context.Friendships
            .Where(e => e.FriendId == customerId && e.StatusId == FriendshipStatusId.Pending)
            .Skip(skipCount)
            .Take(takeCount)
            .Include(e => e.User)
            .Select(e => e.User)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> TotalIncomingFriendsCountAsync(Guid customerId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Friendships
            .Where(e => e.FriendId == customerId && e.StatusId == FriendshipStatusId.Pending)
            .CountAsync(cancellationToken);
    }

    public async Task<IEnumerable<Customer>> GetOutComingFriendsAsync(Guid customerId, int skipCount, int takeCount,
        CancellationToken cancellationToken = default)
    {
        return await _context.Friendships
            .Where(e => e.UserId == customerId && e.StatusId == FriendshipStatusId.Pending)
            .Skip(skipCount)
            .Take(takeCount)
            .Include(e => e.Friend)
            .Select(e => e.Friend)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> TotalOutComingFriendsCountAsync(Guid customerId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Friendships
            .Where(e => e.UserId == customerId && e.StatusId == FriendshipStatusId.Pending)
            .CountAsync(cancellationToken);
    }

    public async Task<int> TotalCountOfCustomersWithUsernameAsync(string username,
        CancellationToken cancellationToken = default)
    {
        return await _context.Customers
            .Where(e => e.Name.ToLower().Contains(username.ToLower()))
            .CountAsync(cancellationToken);
    }

    public async Task<IEnumerable<Customer>> GetCustomersWithIdsAsync(IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        return await _context.Customers.Where(e => ids.Contains(e.Id)).ToListAsync(cancellationToken);
    }

    public void Update(Customer customer)
    {
        _context.Customers.Update(customer);
    }

    public async Task<IReadOnlyCollection<Spending>> GetSpendsForPeriodAsync(Guid customerId, DateTime start,
        DateTime end, CancellationToken cancellationToken = default)
    {
        var expenseItems = await _context.ExpenseParticipants
            .Where(e => e.CustomerId == customerId)
            .Include(e => e.ExpenseParticipantItems)
            .SelectMany(e => e.ExpenseParticipantItems)
            .Include(e => e.Participant)
            .Where(e => e.Participant.CustomerId == customerId)
            .Include(e => e.Participant)
            .ThenInclude(e => e.Expense)
            .ThenInclude(e => e.ExpenseMultipliers)
            .Include(e => e.Participant)
            .ThenInclude(e => e.Expense)
            .ThenInclude(e => e.Category)
            .Include(e => e.Item)
            .Where(e => start <= e.Item.Expense.DateTime && e.Item.Expense.DateTime <= end)
            .Select(e => e.Item)
            .ToListAsync(cancellationToken);
        var spends = expenseItems.GroupBy(e => e.Expense.Category, item => item, (category, items) =>
        {
            var itemsList = items.ToList();
            return new Spending
            {
                CategoryId = category.Id,
                CategoryName = category.Name,
                TotalSpend = itemsList.Sum(item =>
                    item.Amount *
                    (1 + item
                        .Expense
                        .ExpenseMultipliers
                        .Sum(e => e.Multiplier > 1 ? e.Multiplier / 100m : e.Multiplier))),
                ExpensesCount = itemsList.DistinctBy(item => item.ExpenseId).Count()
            };
        }).ToList();
        return spends;
    }

    public async Task<IReadOnlyCollection<Spending>> GetSpendsSharedBetweenUsersAsync(Guid customerId1,
        Guid customerId2,
        CancellationToken cancellationToken = default)
    {
        var firstCustomerExpenses = _context.ExpenseParticipants
            .Where(e => e.CustomerId == customerId1)
            .Include(e => e.Expense)
            .Select(e => e.Expense);
        var secondCustomerExpenses = _context.ExpenseParticipants
            .Where(e => e.CustomerId == customerId2)
            .Include(e => e.Expense)
            .Select(e => e.Expense);
        var expenseItems = await firstCustomerExpenses.Intersect(secondCustomerExpenses)
            .Include(e => e.ExpenseItems)
            .ThenInclude(e => e.ExpenseParticipantItems)
            .SelectMany(e => e.ExpenseItems)
            .SelectMany(e => e.ExpenseParticipantItems)
            .Where(e => e.ExpenseParticipantId == customerId1 || e.ExpenseParticipantId == customerId2)
            .Include(e => e.Item)
            .Select(e => e.Item)
            .Include(e => e.Expense)
            .ThenInclude(e => e.ExpenseMultipliers)
            .Include(e => e.Expense)
            .ThenInclude(e => e.Category)
            .ToListAsync(cancellationToken);
        var spends = expenseItems.GroupBy(e => e.Expense.Category, item => item, (category, items) =>
        {
            var itemsList = items.ToList();
            return new Spending
            {
                CategoryId = category.Id,
                CategoryName = category.Name,
                TotalSpend = itemsList.Sum(item =>
                    item.Amount *
                    (1 + item
                        .Expense
                        .ExpenseMultipliers
                        .Sum(e => e.Multiplier > 1 ? e.Multiplier / 100m : e.Multiplier))),
                ExpensesCount = itemsList.DistinctBy(item => item.ExpenseId).Count()
            };
        }).ToList();
        return spends;
    }
}