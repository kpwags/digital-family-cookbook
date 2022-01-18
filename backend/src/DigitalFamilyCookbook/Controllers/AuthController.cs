using DigitalFamilyCookbook.Handlers.Commands.Auth;
using DigitalFamilyCookbook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace DigitalFamilyCookbook.Controllers;

[Route("auth")]
[ApiController]
public class AuthController : Controller
{
    private readonly IMediator _mediatr;

    public AuthController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResult>> RegisterUser(Register.Command command, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(command, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        if (result.Value is null)
        {
            return BadRequest("Unable to register");
        }

        return Ok(result.Value);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResult>> LoginUser(Login.Command command, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(command, cancellationToken);

        if (result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        if (result.Value is null)
        {
            return BadRequest("Unable to login");
        }

        return Ok(result.Value);
    }
}