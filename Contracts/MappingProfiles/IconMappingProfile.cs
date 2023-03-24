using AutoMapper;
using Contracts.Responses.Icons;
using Domain.Models;

namespace Contracts.MappingProfiles;

public class IconMappingProfile : Profile
{
    public IconMappingProfile()
    {
        CreateMap<Icon, IconResponse>(MemberList.Destination)
            .ForMember(e => e.IconId, expression =>
                expression.MapFrom(e => e.Id))
            .ForMember(e => e.IconUrl, expression =>
                expression.MapFrom(e => e.Url));
    }
}