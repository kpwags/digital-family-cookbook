using DigitalFamilyCookbook.Handlers.Commands.Recipes;

namespace DigitalFamilyCookbook.Tests.Handlers.Recipes;

public class CreateRecipeTests
{
    [Fact]
    public async Task ItSuccessfullyCreatesARecipe()
    {
        var user = MockUser.GenerateUser();
        var userApiModel = UserAccountApiModel.FromDomainModel(user);

        var recipe = MockRecipe.GenerateDomainModel();

        var cmd = new CreateRecipe.Command
        {
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
            Ingredients = recipe.Ingredients.Select(i => new Models.RecipeStep { Name = i.Name, SortOrder = (i.SortOrder.HasValue ? i.SortOrder.Value : 0) }).ToList(),
            Steps = recipe.Steps.Select(d => new Models.RecipeStep { Name = d.Direction, SortOrder = d.SortOrder }).ToList(),
        };

        var httpContextAccessor = MockSession.BuildSession(userApiModel);

        var recipeRepository = new Mock<IRecipeRepository>();

        var handler = new CreateRecipe.Handler(recipeRepository.Object, httpContextAccessor.Object);

        var result = await handler.Handle(cmd, new CancellationToken());
    }
}
