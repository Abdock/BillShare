using AutoMapper;
using Contracts.DTOs.Accounts;
using Contracts.Responses.Accounts;
using Domain.Models;

namespace Contracts.MappingProfiles;

public class AccountMappingProfile : Profile
{
    public AccountMappingProfile()
    {
        CreateMap<AddAccountDto, Account>(MemberList.Source);
        CreateMap<Account, AccountResponse>(MemberList.Source);
    }
}