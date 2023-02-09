using Domain.Base;

namespace Domain.Models;

public class Role : BaseEntity
{
    public string Name { get; init; } = default!;
}