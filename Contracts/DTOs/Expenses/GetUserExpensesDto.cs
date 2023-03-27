using Contracts.DTOs.General;

namespace Contracts.DTOs.Expenses;

public record GetUserExpensesDto : GetPaginationDto
{
    public required Guid UserId { get; init; }
}