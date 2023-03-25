using AutoMapper;
using Contracts.Responses.ExpenseTypes;
using Domain.Models;

namespace Contracts.MappingProfiles;

public class ExpenseTypeMappingProfile : Profile
{
    public ExpenseTypeMappingProfile()
    {
        CreateMap<ExpenseType, ExpenseTypeResponse>(MemberList.Destination);
    }
}