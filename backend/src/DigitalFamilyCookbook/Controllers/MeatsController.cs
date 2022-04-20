using DigitalFamilyCookbook.Handlers.Commands.Meats;
using DigitalFamilyCookbook.Handlers.Queries.Meats;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFamilyCookbook.Controllers;

[Route("meats")]
[ApiController]
public class MeatsController : Controller
{
    private readonly IMediator _mediatr;

    public MeatsController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpGet("get")]
    public async Task<ActionResult<MeatApiModel>> Get(int id, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(new GetMeatById.Query { Id = id }, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }

    [HttpGet("getall")]
    public async Task<ActionResult<IReadOnlyCollection<MeatApiModel>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(new GetAllMeats.Query(), cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }

    [HttpPost("create")]
    [ValidateUser]
    public async Task<IActionResult> Create(CreateMeat.Command command, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(command, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok();
    }

    [HttpPatch("update")]
    [ValidateUser]
    public async Task<IActionResult> Update(UpdateMeat.Command command, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(command, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok();
    }

    [HttpDelete("delete")]
    [ValidateUser]
    public async Task<IActionResult> Delete(int Id, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(new DeleteMeat.Command { Id = Id }, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok();
    }
}
