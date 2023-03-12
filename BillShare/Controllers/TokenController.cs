using Contracts.Authentication;
using Contracts.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace BillShare.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TokenController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public TokenController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    [Authorize]
    [Route("challenge")]
    public ActionResult ChallengeToken()
    {
        return Ok();
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<ActionResult<AuthenticationToken>> RefreshToken([FromBody] RefreshJwtTokenDto dto)
    {
        var token = await _serviceManager.TokenGeneratorService.RefreshJwtTokenAsync(dto.RefreshToken);
        return Ok(token);
    }
}