using Contracts.DTOs.Friendships;
using Contracts.Responses.Friends;
using Contracts.Responses.General;

namespace Services.Abstractions;

public interface IFriendshipService
{
    Task CreateFriendshipAsync(CreateFriendshipDto dto, CancellationToken token = default);

    Task<PagedResponse<UserFriendResponse>> GetPagedUserFriendsAsync(GetUserFriendsDto dto,
        CancellationToken token = default);

    Task<PagedResponse<UserFriendResponse>> GetPagedUserOutcomeFriendsAsync(GetUserFriendsDto dto,
        CancellationToken token = default);

    Task<PagedResponse<UserFriendResponse>> GetPagedUserIncomeFriendsAsync(GetUserFriendsDto dto,
        CancellationToken token = default);

    Task AcceptFriendshipAsync(AcceptFriendshipDto dto, CancellationToken cancellationToken = default);
    Task DeclineFriendshipAsync(DeclineFriendshipDto dto, CancellationToken cancellationToken = default);
}