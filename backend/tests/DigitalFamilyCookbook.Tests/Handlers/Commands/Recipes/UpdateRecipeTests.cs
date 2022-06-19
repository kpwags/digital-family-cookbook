using DigitalFamilyCookbook.Handlers.Commands.Recipes;

namespace DigitalFamilyCookbook.Tests.Handlers.Commands.Recipes;

public class UpdateRecipeTests
{
    private Mock<IRecipeRepository> _recipeRepository;

    public UpdateRecipeTests()
    {
        _recipeRepository = new Mock<IRecipeRepository>();
    }

    [Fact]
    public async Task ItUpdatesARecipeSuccessfully()
    {
        var recipe = MockRecipe.GenerateDomainModel();

        var cmd = new UpdateRecipe.Command
        {
            RecipeId = recipe.RecipeId,
            ActiveTime = recipe.ActiveTime,
            Calories = recipe.Calories,
            Carbohydrates = recipe.Carbohydrates,
            Cholesterol = recipe.Cholesterol,
            Description = recipe.Description,
            Fat = recipe.Fat,
            Fiber = recipe.Fiber,
            IsPublic = recipe.IsPublic,
            Name = recipe.Name,
            Protein = recipe.Protein,
            Servings = recipe.Servings,
            Source = recipe.Source,
            SourceUrl = recipe.SourceUrl,
            Sugar = recipe.Sugar,
            ImageFilename = recipe.ImageUrlLarge is not null ? recipe.ImageUrlLarge.Replace(".jpg", "") : "",
            Ingredients = recipe.Ingredients.Select(i => new Models.RecipeStep { Name = i.Name, SortOrder = (i.SortOrder.HasValue ? i.SortOrder.Value : 0) }).ToList(),
            Steps = recipe.Steps.Select(d => new Models.RecipeStep { Name = d.Direction, SortOrder = d.SortOrder }).ToList(),
        };

        _recipeRepository
            .Setup(r => r.Update(It.IsAny<Recipe>()));

        var handler = new UpdateRecipe.Handler(_recipeRepository.Object);

        var result = await handler.Handle(cmd, new CancellationToken());

        Assert.True(result.IsSuccessful);
    }
}

