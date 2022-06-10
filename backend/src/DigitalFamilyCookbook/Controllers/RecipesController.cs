using DigitalFamilyCookbook.Handlers.Commands.Recipes;
using DigitalFamilyCookbook.Handlers.Queries.Recipes;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFamilyCookbook.Controllers;

[Route("recipes")]
[ApiController]
public class RecipesController : Controller
{
    private readonly IMediator _mediatr;

    public RecipesController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpGet("get")]
    public async Task<ActionResult<RecipeApiModel>> Get(int id, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(new GetRecipeById.Query { Id = id }, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }

    [HttpGet("getall")]
    public async Task<ActionResult<IReadOnlyCollection<RecipeApiModel>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(new GetAllRecipes.Query(), cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }

    [HttpGet("getimage")]
    public async Task<ActionResult<string>> GetImage(string filename, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(new GetRecipeImage.Query { Filename = filename }, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }

    [HttpPost("create")]
    [ValidateUser]
    public async Task<ActionResult<RecipeApiModel>> Create(CreateRecipe.Command command, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(command, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }

    [HttpPatch("update")]
    [ValidateUser]
    public async Task<IActionResult> Update(UpdateRecipe.Command command, CancellationToken cancellationToken)
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
        var result = await _mediatr.Send(new DeleteRecipe.Command { Id = Id }, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok();
    }

    [HttpPost("uploadimage")]
    [ValidateUser]
    public async Task<ActionResult<ImageUploadResponseApiModel>> UploadImage([FromForm] UploadRecipeImage.Command command, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(command, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }

    [HttpPost("deleteimage")]
    [ValidateUser]
    public async Task<ActionResult<ImageUploadResponseApiModel>> DeleteImage(DeleteRecipeImage.Command command, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(command, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok();
    }

    [HttpGet("getuserrecipes")]
    public async Task<ActionResult<IReadOnlyCollection<RecipeApiModel>>> GetUserRecipes(string userAccountId, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(new GetUserRecipes.Query { UserAccountId = userAccountId }, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }
}
