using Domain.Models;

namespace Domain.Repositories;

public interface IIconRepository
{
    Task AddIconAsync(Icon icon, CancellationToken cancellationToken = default);
    Task<IEnumerable<Icon>> GetAllIconsAsync(CancellationToken cancellationToken = default);
    Task<Icon> GetIconAsync(Guid iconId, CancellationToken cancellationToken = default);
}