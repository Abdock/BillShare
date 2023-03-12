using AutoMapper;
using Contracts.DTOs.Friendships;
using Domain.Models;
using Domain.Repositories;
using Services.Abstractions;

namespace Services;

public class FriendshipService : IFriendshipService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FriendshipService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task CreateFriendshipAsync(CreateFriendshipDto dto, CancellationToken token = default)
    {
        var friendship = _mapper.Map<Friendship>(dto);
        await _unitOfWork.FriendshipRepository.AddFriendshipAsync(friendship, token);
        await _unitOfWork.SaveChangesAsync(token);
    }
}