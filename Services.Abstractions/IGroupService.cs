using Contracts.DTOs.Groups;
using Contracts.Responses.General;
using Contracts.Responses.Groups;

namespace Services.Abstractions;

public interface IGroupService
{
    Task<GroupResponse> CreateGroupAsync(CreateGroupDto dto, CancellationToken cancellationToken = default);

    Task<PagedResponse<GroupResponse>> GetPagedGroupsAsync(GetGroupsDto dto,
        CancellationToken cancellationToken = default);

    Task<GroupResponse> GetGroupByIdAsync(Guid groupId, CancellationToken cancellationToken = default);
}