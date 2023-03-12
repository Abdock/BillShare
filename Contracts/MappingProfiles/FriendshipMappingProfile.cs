using AutoMapper;
using Contracts.DTOs.Friendships;
using Domain.Enums;
using Domain.Models;

namespace Contracts.MappingProfiles;

public class FriendshipMappingProfile : Profile
{
    public FriendshipMappingProfile()
    {
        CreateMap<CreateFriendshipDto, Friendship>(MemberList.Source)
            .ForMember(e => e.FriendId, expression =>
            {
                expression.MapFrom(e => e.ReceiverId);
            })
            .ForMember(e => e.UserId, expression =>
            {
                expression.MapFrom(e => e.SenderId);
            })
            .ForMember(e => e.StatusId, expression =>
            {
                expression.MapFrom(_ => FriendshipStatusId.Pending);
            });
    }
}