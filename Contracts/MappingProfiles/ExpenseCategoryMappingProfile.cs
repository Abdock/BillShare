using AutoMapper;
using Contracts.DTOs.ExpenseCategories;
using Contracts.Responses.ExpenseCategories;
using Domain.Models;

namespace Contracts.MappingProfiles;

public class ExpenseCategoryMappingProfile : Profile
{
    public ExpenseCategoryMappingProfile()
    {
        CreateMap<CreateExpenseCategoryDto, CustomExpenseCategory>(MemberList.Source)
            .ForMember(e => e.Name, expression =>
                expression.MapFrom(e => e.CategoryName))
            .ForMember(e => e.CustomerId, expression =>
                expression.MapFrom(e => e.UserId));
        CreateMap<CustomExpenseCategory, ExpenseCategoryResponse>(MemberList.Destination)
            .ForMember(e => e.IconUrl, expression =>
                expression.MapFrom(e => e.Icon.Url));
    }
}