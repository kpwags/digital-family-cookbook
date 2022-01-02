using DigitalFamilyCookbook.Handlers.Commands.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace DigitalFamilyCookbook.Controllers;

[Route("auth")]
[ApiController]
public class AuthController : Controller
{
    private IMediator _mediatr;

    public AuthController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    public async Task<IActionResult> LoginUser(Login.Command command, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(command, cancellationToken);

        if (result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }
}