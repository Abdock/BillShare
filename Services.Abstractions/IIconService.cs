using Contracts.DTOs.Icons;
using Contracts.Responses.Icons;

namespace Services.Abstractions;

public interface IIconService
{
    Task<IconResponse> CreateIconAsync(CreateIconDto dto, CancellationToken cancellationToken = default);

    Task<IEnumerable<IconResponse>> GetAllIconsAsync(CancellationToken cancellationToken = default);

    Task<IconResponse> GetIconByIdAsync(Guid iconId, CancellationToken cancellationToken = default);
}