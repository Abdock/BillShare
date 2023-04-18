using AutoMapper;
using Contracts.DTOs.Groups;
using Contracts.Responses.Groups;
using Domain.Models;

namespace Contracts.MappingProfiles;

public class GroupMappingProfile : Profile
{
    public GroupMappingProfile()
    {
        CreateMap<Group, GroupResponse>(MemberList.Source)
            .ForMember(e => e.GroupId, expression =>
                expression.MapFrom(e => e.Id));
        CreateMap<CreateGroupDto, Group>(MemberList.Source)
            .ForMember(e => e.Participants, expression =>
                expression.Ignore());
    }
}