using DigitalFamilyCookbook.Authorization;
using DigitalFamilyCookbook.Handlers.Commands.Auth;
using DigitalFamilyCookbook.Handlers.Queries.Auth;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFamilyCookbook.Controllers;

[Authorize]
[Route("auth")]
[ApiController]
public class AuthController : BaseController
{
    private readonly IMediator _mediatr;

    public AuthController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [AllowAnonymous]
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

        HttpContext.SetRefreshToken(result.Value.RefreshToken);

        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<AuthResult>> LoginUser(Login.Command command, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(command, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        if (result.Value is null)
        {
            return BadRequest("Unable to login");
        }

        HttpContext.SetRefreshToken(result.Value.RefreshToken);

        return Ok(result.Value);
    }

    [HttpGet("getuser")]
    public async Task<ActionResult<UserAccountApiModel>> GetUser(CancellationToken cancellationToken)
    {
        return await _mediatr.Send(new GetLoggedInUser.Query(), cancellationToken);
    }

    [HttpPost("refreshtoken")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResult>> RefreshToken(CancellationToken cancellationToken)
    {
        var refreshToken = HttpContext.GetRefreshToken();

        if (string.IsNullOrEmpty(refreshToken))
        {
            return BadRequest("Invalid refresh token");
        }

        var result = await _mediatr.Send(
            new Handlers.Commands.Auth.RefreshToken.Command
            {
                Token = refreshToken,
                IpAddress = HttpContext.GetUserIpAddress(),
            },
            cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        if (result is null || result.Value is null)
        {
            return BadRequest("Unable to generate refresh token");
        }

        HttpContext.SetRefreshToken(result.Value.RefreshToken);

        return Ok(result.Value);
    }

    [HttpPost("revoketoken")]
    public async Task<ActionResult> RevokeToken(RevokeToken.Command command, CancellationToken cancellationToken)
    {
        try
        {
            await _mediatr.Send(command, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}