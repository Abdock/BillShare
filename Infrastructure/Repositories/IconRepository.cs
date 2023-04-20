using Domain.Exceptions;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class IconRepository : IIconRepository
{
    private readonly AppDbContext _context;

    public IconRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddIconAsync(Icon icon, CancellationToken cancellationToken = default)
    {
        await _context.Icons.AddAsync(icon, cancellationToken);
    }

    public async Task<IEnumerable<Icon>> GetAllIconsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Icons.ToListAsync(cancellationToken);
    }

    public async Task<Icon> GetIconAsync(Guid iconId, CancellationToken cancellationToken = default)
    {
        var icon = await _context.Icons.FirstOrDefaultAsync(e => e.Id == iconId, cancellationToken);
        if (icon is null)
        {
            throw new NotFoundException($"Icon with id {iconId} not found");
        }

        return icon;
    }
}