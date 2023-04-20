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
        if (friendship.FriendId == friendship.UserId)
        {
            throw new InvalidRequestException("Friend id and user id must be different");
        }
        
        var isUserNotExists = await _context.Customers.AllAsync(e => e.Id != friendship.UserId, token);
        if (isUserNotExists)
        {
            throw new NotFoundException($"Customer with id {friendship.UserId} not found");
        }
        
        var isFriendNotExists = await _context.Customers.AllAsync(e => e.Id != friendship.FriendId, token);
        if (isFriendNotExists)
        {
            throw new NotFoundException($"Customer with id {friendship.FriendId} not found");
        }

        var isRequestExists = await _context.Friendships.AnyAsync(e =>
            e.FriendId == friendship.FriendId && e.UserId == friendship.UserId &&
            e.StatusId == FriendshipStatusId.Pending, token);
        if (isRequestExists)
        {
            throw new FriendshipRequestExistsException(
                $"Friendship request between user with id {friendship.UserId} and user with id {friendship.FriendId} already exists");
        }
        var isReverseRequestExists = await _context.Friendships.AnyAsync(e =>
            e.FriendId == friendship.UserId && e.UserId == friendship.FriendId &&
            e.StatusId == FriendshipStatusId.Pending, token);
        if (isReverseRequestExists)
        {
            throw new FriendshipRequestExistsException(
                $"Friendship request between user with id {friendship.UserId} and user with id {friendship.FriendId} already exists");
        }
        
        await _context.Friendships.AddAsync(friendship, token);
    }

    public async Task<Friendship> GetFriendshipAsync(Guid id, CancellationToken token = default)
    {
        var friendship = await _context.Friendships.FirstOrDefaultAsync(e => e.Id == id, token);
        if (friendship is null)
        {
            throw new NotFoundException($"Friendship with id {id} not found");
        }

        return friendship;
    }

    public async Task AcceptFriendshipAsync(Guid userId, Guid friendId, CancellationToken token = default)
    {
        var friendship = await _context.Friendships
            .FirstOrDefaultAsync(e =>
                e.FriendId == userId && e.UserId == friendId && e.StatusId == FriendshipStatusId.Pending, token);
        if (friendship is null)
        {
            throw new NotFoundException($"Friendship request by id {friendId} not found");
        }

        friendship.StatusId = FriendshipStatusId.Accepted;
        _context.Friendships.Update(friendship);
        var reverseFriendship = new Friendship
        {
            FriendId = friendId,
            UserId = userId,
            StatusId = FriendshipStatusId.Accepted
        };
        await _context.AddAsync(reverseFriendship, token);
    }

    public async Task RejectFriendshipAsync(Guid userId, Guid friendId, CancellationToken token = default)
    {
        var friendship = await _context.Friendships
            .FirstOrDefaultAsync(e =>
                e.FriendId == userId && e.UserId == friendId && e.StatusId == FriendshipStatusId.Pending, token);
        if (friendship is null)
        {
            throw new NotFoundException($"Friendship request by id {friendId} not found");
        }

        friendship.StatusId = FriendshipStatusId.Rejected;
        _context.Friendships.Update(friendship);
        var reverseFriendship = new Friendship
        {
            FriendId = friendId,
            UserId = userId,
            StatusId = FriendshipStatusId.Accepted
        };
        await _context.AddAsync(reverseFriendship, token);
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