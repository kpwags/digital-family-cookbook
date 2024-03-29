using DigitalFamilyCookbook.Authorization;
using DigitalFamilyCookbook.Handlers.Commands.Recipes;
using DigitalFamilyCookbook.Handlers.Queries.Recipes;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFamilyCookbook.Controllers;

[Route("recipes")]
[ApiController]
[Authorize]
public class RecipesController : Controller
{
    private readonly IMediator _mediatr;

    public RecipesController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpGet("get")]
    [AllowAnonymous]
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
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyCollection<RecipeApiModel>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(new GetRecipesForAdmin.Query(), cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }

    [HttpGet("getimage")]
    [AllowAnonymous]
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
    public async Task<ActionResult<ImageUploadResponseApiModel>> DeleteImage(DeleteRecipeImage.Command command, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(command, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok();
    }

    [HttpPost("markfavorite")]
    public async Task<ActionResult<ImageUploadResponseApiModel>> MarkRecipeAsFavorite(MarkRecipeAsFavorite.Command command, CancellationToken cancellationToken)
    {
        await _mediatr.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPost("removefavorite")]
    public async Task<ActionResult<ImageUploadResponseApiModel>> RemoveRecipeAsFavorite(RemoveRecipeAsFavorite.Command command, CancellationToken cancellationToken)
    {
        await _mediatr.Send(command, cancellationToken);

        return Ok();
    }

    [HttpGet("getuserrecipes")]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyCollection<RecipeApiModel>>> GetUserRecipes(string userAccountId, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(new GetUserRecipes.Query { UserAccountId = userAccountId }, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }

    [HttpGet("getrecipesbycategory")]
    [AllowAnonymous]
    public async Task<ActionResult<RecipeListPageResults>> GetRecipesByCategory([FromQuery] GetRecipesByCategory.Query query, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(query, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }

    [HttpGet("getrecipesbymeat")]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyCollection<RecipeListPageResults>>> GetRecipesByMeat([FromQuery] GetRecipesByMeat.Query query, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(query, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }

    [HttpGet("getrecipesbyuser")]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyCollection<RecipeListPageResults>>> GetRecipesByUser([FromQuery] GetRecipesByUser.Query query, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(query, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }

    [HttpGet("getallrecipes")]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyCollection<RecipeListPageResults>>> GetAllRecipes([FromQuery] GetAllRecipes.Query query, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(query, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }

    [HttpGet("getfavoriterecipes")]
    public async Task<ActionResult<RecipeListPageResults>> GetFavoriteRecipes([FromQuery] GetRecipeFavorites.Query query, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(query, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }
    
    [HttpGet("getrecentrecipes")]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyCollection<RecipeApiModel>>> GetRecentRecipes([FromQuery] GetRecentRecipes.Query query, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(query, cancellationToken);

        return Ok(result);
    }
    
    [HttpGet("getmostfavoritedrecipes")]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyCollection<RecipeApiModel>>> GetMostFavoritedRecipes([FromQuery] GetMostFavoritedRecipes.Query query, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(query, cancellationToken);

        return Ok(result);
    }
    
    [HttpGet("search")]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyCollection<RecipeApiModel>>> Search([FromQuery] GetRecipesBySearchKeywords.Query query, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(query, cancellationToken);

        return Ok(result.Value);
    }
    
    [HttpGet("quicksearch")]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyCollection<RecipeApiModel>>> QuickSearch([FromQuery] QuickSearchRecipes.Query query, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(query, cancellationToken);

        return Ok(result);
    }
}
