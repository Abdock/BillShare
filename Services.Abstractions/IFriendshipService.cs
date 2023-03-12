using Contracts.DTOs.Friendships;

namespace Services.Abstractions;

public interface IFriendshipService
{
    Task CreateFriendshipAsync(CreateFriendshipDto dto, CancellationToken token = default);
}