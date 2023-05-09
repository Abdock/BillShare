using AutoMapper;
using Contracts.DTOs.Reports;
using Domain.ValueObjects;

namespace Contracts.MappingProfiles;

public class SpendsMappingProfile : Profile
{
    public SpendsMappingProfile()
    {
        CreateMap<Spending, CategorySpend>(MemberList.Destination);
    }
}