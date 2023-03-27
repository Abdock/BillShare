using AutoMapper;
using Contracts.Constants;
using Contracts.Responses.Expenses;
using Domain.Models;

namespace Contracts.MappingProfiles;

public class ExpenseMappingProfile : Profile
{
    public ExpenseMappingProfile()
    {
        CreateMap<Expense, ExpenseResponse>(MemberList.Destination)
            .ForMember(e => e.DateTime, expression =>
                expression.MapFrom(e => e.DateTime.ToString(FormatConstants.DateTimeFormat)))
            .ForMember(e => e.Participants, expression =>
                expression.MapFrom(e => e.ExpenseParticipants))
            .ForMember(e => e.Multipliers, expression =>
                expression.MapFrom(e => e.ExpenseMultipliers));
    }
}