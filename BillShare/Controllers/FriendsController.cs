using BillShare.Extensions;
using Contracts.DTOs.Friendships;
using Contracts.DTOs.General;
using Contracts.Responses.Friends;
using Contracts.Responses.General;
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

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<PagedResponse<UserFriendResponse>>> GetFriends([FromQuery] PaginationDto dto)
    {
        var url = $"{Request.Host}{Request.Path.Value!}";
        var request = new GetUserFriendsDto
        {
            UserId = User.GetUserId(),
            Pagination = dto,
            EndpointUrl = new Uri(url)
        };
        var response = await _serviceManager.FriendshipService.GetPagedUserFriendsAsync(request);
        return Ok(response);
    }
    
    [HttpGet]
    [Authorize]
    [Route("outcoming")]
    public async Task<ActionResult<PagedResponse<UserFriendResponse>>> GetOutComingFriends([FromQuery] PaginationDto dto)
    {
        var url = $"{Request.Host}{Request.Path.Value!}";
        var request = new GetUserFriendsDto
        {
            UserId = User.GetUserId(),
            Pagination = dto,
            EndpointUrl = new Uri(url)
        };
        var response = await _serviceManager.FriendshipService.GetPagedUserOutcomeFriendsAsync(request);
        return Ok(response);
    }
    
    [HttpGet]
    [Authorize]
    [Route("incoming")]
    public async Task<ActionResult<PagedResponse<UserFriendResponse>>> GetInComingFriends([FromQuery] PaginationDto dto)
    {
        var url = $"{Request.Host}{Request.Path.Value!}";
        var request = new GetUserFriendsDto
        {
            UserId = User.GetUserId(),
            Pagination = dto,
            EndpointUrl = new Uri(url)
        };
        var response = await _serviceManager.FriendshipService.GetPagedUserIncomeFriendsAsync(request);
        return Ok(response);
    }

    [HttpPost]
    [Authorize]
    [Route("{userId:guid}/accept")]
    public async Task<ActionResult> AcceptFriendship([FromRoute] Guid userId)
    {
        var dto = new AcceptFriendshipDto
        {
            UserId = User.GetUserId(),
            FriendId = userId
        };
        await _serviceManager.FriendshipService.AcceptFriendshipAsync(dto);
        return NoContent();
    }

    [HttpPost]
    [Authorize]
    [Route("{userId:guid}/decline")]
    public async Task<ActionResult> DeclineFriendship([FromRoute] Guid userId)
    {
        var dto = new DeclineFriendshipDto
        {
            UserId = User.GetUserId(),
            FriendId = userId
        };
        await _serviceManager.FriendshipService.DeclineFriendshipAsync(dto);
        return NoContent();
    }
}