using AutoMapper;
using Contracts.DTOs;
using Contracts.Responses;
using Domain.Models;

namespace Contracts.MappingProfiles;

public class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<Customer, CustomerResponse>(MemberList.Destination);
        CreateMap<CreateCustomerDto, Customer>(MemberList.Source);
    }
}