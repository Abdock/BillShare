using AutoMapper;
using Contracts.DTOs.ExpenseItems;
using Contracts.Responses.ExpenseItems;
using Domain.Enums;
using Domain.Models;

namespace Contracts.MappingProfiles;

public class ExpenseItemMappingProfile : Profile
{
    public ExpenseItemMappingProfile()
    {
        CreateMap<ExpenseItem, ExpenseItemResponse>(MemberList.Destination)
            .ForMember(e => e.SelectedByParticipants, expression =>
                expression.MapFrom(e =>
                    e.ExpenseParticipantItems.Where(e => e.StatusId == ExpenseParticipantItemStatusId.Selected).Select(p => p.ExpenseParticipantId)))
            .ForMember(e => e.Actions, expression => expression.Ignore());
        CreateMap<CreateExpenseItemDto, ExpenseItem>(MemberList.Source)
            .ForMember(e => e.StatusId, expression =>
                expression.MapFrom(e => ExpenseItemStatusId.Active));
        
    }
}