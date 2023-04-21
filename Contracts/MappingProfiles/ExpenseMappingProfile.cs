using AutoMapper;
using Contracts.Constants;
using Contracts.DTOs.Expenses;
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
        CreateMap<Expense, ShortExpenseResponse>(MemberList.Destination)
            .ForMember(e => e.DateTime, expression =>
                expression.MapFrom(e => e.DateTime.ToString(FormatConstants.DateTimeFormat)));
        CreateMap<CreateExpenseDto, Expense>(MemberList.Source)
            .ForMember(e => e.Amount, expression =>
                expression.MapFrom(e => (decimal) e.Amount))
            .ForMember(e=>e.DateTime, expression => expression.MapFrom(_=>DateTime.UtcNow))
            .ForMember(e=>e.ExpenseParticipants, expression => expression.Ignore())
            .ForMember(e=>e.ExpenseMultipliers, expression => expression.Ignore())
            .ForMember(e=>e.ExpenseItems, expression => expression.Ignore());
    }
}