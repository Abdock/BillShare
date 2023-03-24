﻿using Domain.Models;

namespace Domain.Repositories;

public interface IFriendshipRepository
{
    Task AddFriendshipAsync(Friendship friendship, CancellationToken token = default);
    Task<Friendship> GetFriendshipAsync(Guid id, CancellationToken token = default);
    Task AcceptFriendshipAsync(Guid friendshipId, CancellationToken token = default);
    Task RejectFriendshipAsync(Guid friendshipId, CancellationToken token = default);
}