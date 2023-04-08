using AutoMapper;
using Contracts.DTOs;
using Contracts.DTOs.Customers;
using Contracts.DTOs.General;
using Contracts.Responses.Customers;
using Contracts.Responses.Friends;
using Domain.Models;

namespace Contracts.MappingProfiles;

public class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<Customer, CustomerResponse>(MemberList.Destination);
        CreateMap<CreateCustomerDto, Customer>(MemberList.Source);
        CreateMap<Customer, RelatedCustomerResponse>(MemberList.Destination)
            .ForMember(e=>e.UserId, expression => 
                expression.MapFrom(e=>e.Id))
            .ForMember(e=>e.UserName, expression => 
                expression.MapFrom(e=>e.Name))
            .ForMember(e => e.IsFriend, expression => expression.Ignore());
        CreateMap<Customer, UserFriendResponse>(MemberList.Destination)
            .ForMember(e => e.UserId, expression =>
            {
                expression.MapFrom(e => e.Id);
            })
            .ForMember(e => e.UserName, expression =>
            {
                expression.MapFrom(e => e.Name);
            })
            .ForMember(e => e.UserAvatarUrl, expression =>
            {
                expression.MapFrom(e => e.AvatarUrl);
            });
        CreateMap<ChangeCustomerAvatarDto, StorageFile>(MemberList.Destination)
            .ForMember(e => e.Id, expression =>
                expression.MapFrom(e => Guid.NewGuid()));
        CreateMap<Customer, CustomerAvatarIcon>(MemberList.Destination);
    }
}