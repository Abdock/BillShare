using Contracts.Responses.Friends;

namespace Contracts.Responses.Groups;

public record GroupResponse
{
    public required Guid GroupId { get; init; }
    public required string GroupName { get; init; }
    public ICollection<UserFriendResponse> Participants { get; init; } = new HashSet<UserFriendResponse>();
}