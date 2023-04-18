using AutoMapper;
using Contracts.DTOs.General;
using Contracts.DTOs.Groups;
using Contracts.Responses.General;
using Contracts.Responses.Groups;
using Domain.Models;
using Domain.Repositories;
using Services.Abstractions;

namespace Services;

public class GroupService : IGroupService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPaginationService _paginationService;

    public GroupService(IUnitOfWork unitOfWork, IMapper mapper, IPaginationService paginationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _paginationService = paginationService;
    }

    public async Task<GroupResponse> CreateGroupAsync(CreateGroupDto dto, CancellationToken cancellationToken = default)
    {
        dto.Participants.Add(dto.CreatorId);
        var group = _mapper.Map<Group>(dto);
        var participants = await _unitOfWork.CustomerRepository
            .GetCustomersWithIdsAsync(dto.Participants, cancellationToken);
        foreach (var customer in participants)
        {
            group.Participants.Add(customer);
        }
        
        await _unitOfWork.GroupRepository.AddGroupAsync(group, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<GroupResponse>(group);
    }

    public async Task<PagedResponse<GroupResponse>> GetPagedGroupsAsync(GetGroupsDto dto, CancellationToken cancellationToken = default)
    {
        var skipCount = (dto.Pagination.PageNumber - 1) * dto.Pagination.PageSize;
        var groups = await _unitOfWork.GroupRepository
            .GetPagedGroupsAsync(skipCount, dto.Pagination.PageSize, cancellationToken);
        var totalCount = await _unitOfWork.GroupRepository.TotalGroupsCountAsync(cancellationToken);
        var paginationDto = new CreatePagedResponseDto<GroupResponse>
        {
            Responses = groups.Select(e=>_mapper.Map<GroupResponse>(e)),
            TotalCount = totalCount,
            EndpointUrl = dto.EndpointUrl.ToString(),
            CurrentPage = dto.Pagination
        };
        return _paginationService.CreatePagedResponse(paginationDto);
    }

    public async Task<GroupResponse> GetGroupByIdAsync(Guid groupId, CancellationToken cancellationToken = default)
    {
        var group = await _unitOfWork.GroupRepository.GetGroupByIdAsync(groupId, cancellationToken);
        return _mapper.Map<GroupResponse>(group);
    }
}