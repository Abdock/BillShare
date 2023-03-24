using Contracts.DTOs.General;

namespace Contracts.DTOs.Friendships;

public record GetUserFriendsDto : GetPaginationDto
{
    public required Guid UserId { get; init; }
}