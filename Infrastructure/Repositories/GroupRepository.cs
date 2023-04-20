using Domain.Exceptions;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GroupRepository : IGroupRepository
{
    private readonly AppDbContext _context;

    public GroupRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddGroupAsync(Group group, CancellationToken cancellationToken = default)
    {
        await _context.Groups.AddAsync(group, cancellationToken);
    }

    public async Task<Group> GetGroupByIdAsync(Guid groupId, CancellationToken cancellationToken = default)
    {
        var group = await _context.Groups
            .Where(e => e.Id == groupId)
            .Include(e => e.Participants)
            .Include(e => e.Creator)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
        if (group is null)
        {
            throw new NotFoundException($"Group with id {groupId} not found");
        }

        return group;
    }

    public async Task<IEnumerable<Group>> GetPagedGroupsAsync(int skipCount, int takeCount, CancellationToken cancellationToken = default)
    {
        return await _context.Groups
            .Skip(skipCount)
            .Take(takeCount)
            .Include(e=>e.Participants)
            .Include(e=>e.Creator)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<int> TotalGroupsCountAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Groups.CountAsync(cancellationToken);
    }
}