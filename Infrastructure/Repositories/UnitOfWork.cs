using Domain.Repositories;
using Infrastructure.Database.Context;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        CustomerRepository = new CustomerRepository(context);
        RefreshTokenRepository = new RefreshTokenRepository(context);
        FriendshipRepository = new FriendshipRepository(context);
    }

    public ICustomerRepository CustomerRepository { get; }
    public IRefreshTokenRepository RefreshTokenRepository { get; }
    public IFriendshipRepository FriendshipRepository { get; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}