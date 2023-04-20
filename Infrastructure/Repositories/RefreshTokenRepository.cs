using Domain.Exceptions;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _context;

    public RefreshTokenRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RefreshToken>> GetCustomerRefreshTokensAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        var customerRefreshTokens = await _context.RefreshTokens
            .Where(e => e.OwnerId == customerId)
            .ToListAsync(cancellationToken);
        return customerRefreshTokens;
    }

    public async Task<RefreshToken> GetRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var token = await _context.RefreshTokens
            .FirstOrDefaultAsync(e => e.Token == refreshToken, cancellationToken);
        if (token is null)
        {
            throw new NotFoundException($"Refresh token {refreshToken} not found");
        }

        return token;
    }

    public async Task<Customer> GetRefreshTokenOwnerAsync(string refreshToken,
        CancellationToken cancellationToken = default)
    {
        var token = await _context.RefreshTokens.Where(e => e.Token == refreshToken)
            .Include(e => e.Owner)
            .FirstOrDefaultAsync(cancellationToken);
        if (token is null)
        {
            throw new NotFoundException($"Refresh token {refreshToken} not found");
        }

        return token.Owner;
    }

    public async Task AddRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
    }

    public void UpdateRefreshToken(RefreshToken refreshToken)
    {
        _context.Update(refreshToken);
    }
}