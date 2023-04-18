namespace BillShare.Requests.Groups;

public class CreateGroupRequest
{
    public required string GroupName { get; init; }
    public ICollection<Guid> Participants { get; init; } = new List<Guid>();
}