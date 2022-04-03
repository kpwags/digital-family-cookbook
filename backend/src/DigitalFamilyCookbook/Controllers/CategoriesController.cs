using DigitalFamilyCookbook.Handlers.Commands.Categories;
using DigitalFamilyCookbook.Handlers.Queries.Categories;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFamilyCookbook.Controllers;

[Route("categories")]
[ApiController]
[Authorize]
public class CategoriesController : Controller
{
    private readonly IMediator _mediatr;

    public CategoriesController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpGet("get")]
    public async Task<ActionResult<CategoryApiModel>> GetAll(int id, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(new GetCategoryById.Query { Id = id }, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }

    [HttpGet("getall")]
    public async Task<ActionResult<IReadOnlyCollection<CategoryApiModel>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(new GetAllCategories.Query(), cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }

    [HttpPost("save")]
    public async Task<ActionResult<IReadOnlyCollection<CategoryApiModel>>> Save(SaveCategory.Command command, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(command, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(DeleteCategory.Command command, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(command, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok();
    }
}
