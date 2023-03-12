using Domain.Models;

namespace Domain.Repositories;

public interface IRefreshTokenRepository
{
    Task<IEnumerable<RefreshToken>> GetCustomerRefreshTokensAsync(Guid customerId, CancellationToken cancellationToken = default);
    Task<RefreshToken> GetRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task<Customer> GetRefreshTokenOwnerAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task AddRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
    void UpdateRefreshToken(RefreshToken refreshToken);
}