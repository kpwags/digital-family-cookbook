using DigitalFamilyCookbook.Handlers.Commands.Categories;
using DigitalFamilyCookbook.Handlers.Queries.Categories;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFamilyCookbook.Controllers;

[Route("categories")]
[ApiController]
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

    [HttpPost("create")]
    [ValidateUser]
    public async Task<IActionResult> Create(CreateCategory.Command command, CancellationToken cancellationToken)
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
    public async Task<IActionResult> Update(UpdateCategory.Command command, CancellationToken cancellationToken)
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
        var result = await _mediatr.Send(new DeleteCategory.Command { Id = Id }, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok();
    }
}
