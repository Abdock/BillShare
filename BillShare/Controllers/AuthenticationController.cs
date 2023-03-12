using Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions.Authentication;

namespace BillShare.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<AuthenticationToken>> Register([FromBody] SignUpUserCredentials credentials)
    {
        var token = await _authenticationService.SignUpAsync(credentials);
        return Ok(token);
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<AuthenticationToken>> Login([FromBody] SignInUserCredentials credentials)
    {
        var token = await _authenticationService.SignInAsync(credentials);
        return Ok(token);
    }
}