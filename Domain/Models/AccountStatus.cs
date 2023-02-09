using Domain.Enums;

namespace Domain.Models;

public class AccountStatus
{
    public AccountStatusId Id { get; init; }
    public string Name { get; init; } = default!;
}