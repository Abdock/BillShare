using BillShare.Extensions;
using Contracts.DTOs.Friendships;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace BillShare.Controllers;

[ApiController]
[Route("[controller]")]
public class FriendsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public FriendsController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> CreateFriendship([FromBody] CreateFriendshipRequestDto request)
    {
        var dto = new CreateFriendshipDto
        {
            ReceiverId = request.UserId,
            SenderId = User.GetUserId()
        };
        await _serviceManager.FriendshipService.CreateFriendshipAsync(dto);
        return Ok();
    }
}