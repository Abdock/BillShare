using Domain.Models;

namespace Domain.Repositories;

public interface IGroupRepository
{
    Task AddGroupAsync(Group group, CancellationToken cancellationToken = default);

    Task<Group> GetGroupByIdAsync(Guid groupId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Group>> GetPagedGroupsAsync(int skipCount, int takeCount,
        CancellationToken cancellationToken = default);

    Task<int> TotalGroupsCountAsync(CancellationToken cancellationToken = default);
}