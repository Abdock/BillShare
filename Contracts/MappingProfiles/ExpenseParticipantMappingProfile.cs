using AutoMapper;
using Contracts.DTOs.ExpenseParticipants;
using Contracts.Responses.ExpenseParticipants;
using Domain.Enums;
using Domain.Models;

namespace Contracts.MappingProfiles;

public class ExpenseParticipantMappingProfile : Profile
{
    public ExpenseParticipantMappingProfile()
    {
        CreateMap<ExpenseParticipant, ExpenseParticipantResponse>(MemberList.Destination)
            .ForMember(e => e.ParticipantId, expression =>
                expression.MapFrom(e => e.Id))
            .ForMember(e => e.UserId, expression =>
                expression.MapFrom(e => e.CustomerId))
            .ForMember(e => e.AvatarUrl, expression =>
                expression.MapFrom(e => e.Customer.AvatarUrl))
            .ForMember(e => e.Actions, expression => expression.Ignore());
        CreateMap<AddExpenseParticipantDto, ExpenseParticipant>(MemberList.Source)
            .ForMember(e => e.CustomerId, expression =>
                expression.MapFrom(e => e.UserId))
            .ForMember(e => e.StatusId, expression =>
                expression.MapFrom(e => ExpenseParticipantStatusId.Unpaid));
    }
}