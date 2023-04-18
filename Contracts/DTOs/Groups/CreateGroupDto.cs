namespace Contracts.DTOs.Groups;

public record CreateGroupDto
{
    public required Guid CreatorId { get; init; }
    public required string GroupName { get; init; }
    public ICollection<Guid> Participants { get; init; } = new HashSet<Guid>();
}