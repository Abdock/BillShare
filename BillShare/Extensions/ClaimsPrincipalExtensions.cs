using System.Security.Claims;
using Contracts.Constants;
using Domain.Exceptions;

namespace BillShare.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        var value = principal.FindFirstValue(ClaimsConstants.Uid);
        var isValidGuid = Guid.TryParse(value, out var userId);
        if (!isValidGuid)
        {
            throw new NotEnoughPermissionsException("User token invalid");
        }

        return userId;
    }
}