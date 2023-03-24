using Domain.Enums;
using Domain.Exceptions;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FriendshipRepository : IFriendshipRepository
{
    private readonly AppDbContext _context;

    public FriendshipRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddFriendshipAsync(Friendship friendship, CancellationToken token = default)
    {
        var isUserExists = await _context.Customers.AnyAsync(e => e.Id == friendship.UserId, token);
        if (!isUserExists)
        {
            throw new NotFoundException($"Customer with id {friendship.UserId} not found");
        }
        
        var isFriendExists = await _context.Customers.AnyAsync(e => e.Id == friendship.FriendId, token);
        if (!isFriendExists)
        {
            throw new NotFoundException($"Customer with id {friendship.FriendId} not found");
        }
        
        await _context.Friendships.AddAsync(friendship, token);
    }

    public async Task<Friendship> GetFriendshipAsync(Guid id, CancellationToken token = default)
    {
        var friendship = await _context.Friendships.FirstOrDefaultAsync(e => e.Id == id, token);
        if (friendship == null)
        {
            throw new NotFoundException($"Friendship with id {id} not found");
        }

        return friendship;
    }

    public async Task AcceptFriendshipAsync(Guid friendshipId, CancellationToken token = default)
    {
        var friendship = await _context.Friendships.FirstOrDefaultAsync(e => e.Id == friendshipId, token);
        if (friendship == null)
        {
            throw new NotFoundException($"Friendship request by id {friendshipId} not found");
        }

        friendship.StatusId = FriendshipStatusId.Accepted;
        _context.Friendships.Update(friendship);
    }

    public async Task RejectFriendshipAsync(Guid friendshipId, CancellationToken token = default)
    {
        var friendship = await _context.Friendships.FirstOrDefaultAsync(e => e.Id == friendshipId, token);
        if (friendship == null)
        {
            throw new NotFoundException($"Friendship request by id {friendshipId} not found");
        }

        friendship.StatusId = FriendshipStatusId.Rejected;
        _context.Friendships.Update(friendship);
    }

    public async Task<IEnumerable<Friendship>> GetFriendshipsAsync(int skipCount, int takeCount,
        CancellationToken token = default)
    {
        var friendships = await _context.Friendships
            .Skip(skipCount)
            .Take(takeCount)
            .ToListAsync(token);
        return friendships;
    }

    public async Task<int> TotalCountAsync(CancellationToken token = default)
    {
        return await _context.Friendships.CountAsync(token);
    }
}