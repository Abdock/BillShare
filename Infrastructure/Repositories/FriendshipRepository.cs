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
}