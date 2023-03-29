using AutoMapper;
using Contracts.DTOs.Friendships;
using Contracts.DTOs.General;
using Contracts.Responses.Friends;
using Contracts.Responses.General;
using Domain.Models;
using Domain.Repositories;
using Services.Abstractions;

namespace Services;

public class FriendshipService : IFriendshipService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPaginationService _paginationService;

    public FriendshipService(IUnitOfWork unitOfWork, IMapper mapper, IPaginationService paginationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _paginationService = paginationService;
    }

    public async Task CreateFriendshipAsync(CreateFriendshipDto dto, CancellationToken token = default)
    {
        var friendship = _mapper.Map<Friendship>(dto);
        await _unitOfWork.FriendshipRepository.AddFriendshipAsync(friendship, token);
        await _unitOfWork.SaveChangesAsync(token);
    }

    public async Task<PagedResponse<UserFriendResponse>> GetPagedUserFriendsAsync(GetUserFriendsDto dto,
        CancellationToken token = default)
    {
        var skipCount = (dto.Pagination.PageNumber - 1) * dto.Pagination.PageSize;
        var friends = await _unitOfWork.CustomerRepository
            .GetFriendsAsync(dto.UserId, skipCount, dto.Pagination.PageSize, token);
        var totalCount = await _unitOfWork.CustomerRepository.TotalFriendsCountAsync(dto.UserId, token);
        var friendsResponse = friends.Select(user => _mapper.Map<UserFriendResponse>(user));
        var createPagination = new CreatePagedResponseDto<UserFriendResponse>
        {
            Responses = friendsResponse,
            TotalCount = totalCount,
            EndpointUrl = dto.EndpointUrl.ToString(),
            CurrentPage = dto.Pagination
        };
        return _paginationService.CreatePagedResponse(createPagination);
    }

    public async Task<PagedResponse<UserFriendResponse>> GetPagedUserOutcomeFriendsAsync(GetUserFriendsDto dto,
        CancellationToken token = default)
    {
        var skipCount = (dto.Pagination.PageNumber - 1) * dto.Pagination.PageSize;
        var friends = await _unitOfWork.CustomerRepository
            .GetOutComingFriendsAsync(dto.UserId, skipCount, dto.Pagination.PageSize, token);
        var totalCount = await _unitOfWork.CustomerRepository.TotalOutComingFriendsCountAsync(dto.UserId, token);
        var friendsResponse = friends.Select(user => _mapper.Map<UserFriendResponse>(user));
        var createPagination = new CreatePagedResponseDto<UserFriendResponse>
        {
            Responses = friendsResponse,
            TotalCount = totalCount,
            EndpointUrl = dto.EndpointUrl.ToString(),
            CurrentPage = dto.Pagination
        };
        return _paginationService.CreatePagedResponse(createPagination);
    }

    public async Task<PagedResponse<UserFriendResponse>> GetPagedUserIncomeFriendsAsync(GetUserFriendsDto dto,
        CancellationToken token = default)
    {
        var skipCount = (dto.Pagination.PageNumber - 1) * dto.Pagination.PageSize;
        var friends = await _unitOfWork.CustomerRepository
            .GetIncomingFriendsAsync(dto.UserId, skipCount, dto.Pagination.PageSize, token);
        var totalCount = await _unitOfWork.CustomerRepository.TotalIncomingFriendsCountAsync(dto.UserId, token);
        var friendsResponse = friends.Select(user => _mapper.Map<UserFriendResponse>(user));
        var createPagination = new CreatePagedResponseDto<UserFriendResponse>
        {
            Responses = friendsResponse,
            TotalCount = totalCount,
            EndpointUrl = dto.EndpointUrl.ToString(),
            CurrentPage = dto.Pagination
        };
        return _paginationService.CreatePagedResponse(createPagination);
    }

    public async Task AcceptFriendshipAsync(AcceptFriendshipDto dto, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.FriendshipRepository.AcceptFriendshipAsync(dto.UserId, dto.FriendId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeclineFriendshipAsync(DeclineFriendshipDto dto, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.FriendshipRepository.RejectFriendshipAsync(dto.UserId, dto.FriendId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}