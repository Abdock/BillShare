using AutoMapper;
using Contracts.DTOs.ExpenseMultipliers;
using Contracts.Responses.Expenses;
using Domain.Models;

namespace Contracts.MappingProfiles;

public class ExpenseMultiplierMappingProfile : Profile
{
    public ExpenseMultiplierMappingProfile()
    {
        CreateMap<ExpenseMultiplier, ExpenseMultiplierResponse>(MemberList.Destination)
            .ForMember(e => e.CostMultiplierPercent, expression =>
                expression.MapFrom(e => (int) (e.Multiplier * 100)));
        CreateMap<CreateExpenseMultiplierDto, ExpenseMultiplier>(MemberList.Source);
    }
}