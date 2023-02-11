using Domain.Enums;

namespace Domain.Models;

public class Role
{
    public RoleId Id { get; init; }
    public string Name { get; init; } = default!;
}