using Contracts.Authentication;
using Contracts.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions.Authentication;

namespace BillShare.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TokenController : ControllerBase
{
    private readonly ITokenGeneratorService _tokenGeneratorService;

    public TokenController(ITokenGeneratorService tokenGeneratorService)
    {
        _tokenGeneratorService = tokenGeneratorService;
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
        var token = await _tokenGeneratorService.RefreshJwtTokenAsync(dto.RefreshToken);
        return Ok(token);
    }
}