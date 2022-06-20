using DigitalFamilyCookbook.Handlers.Queries.Recipes;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.Recipes;

public class GetAllRecipesTests
{
    private Mock<IRecipeRepository> _recipeRepository;

    public GetAllRecipesTests()
    {
        _recipeRepository = new Mock<IRecipeRepository>();
    }

    [Fact]
    public async Task ItReturnsAllRecipes()
    {
        var recipes = MockRecipe.GenerateDomainModelList(10);

        _recipeRepository
            .Setup(r => r.GetAll())
            .Returns(recipes);

        var query = new GetAllRecipes.Query();

        var handler = new GetAllRecipes.Handler(_recipeRepository.Object);

        var result = await handler.Handle(query, new CancellationToken());

        Assert.True(result.IsSuccessful);
        Assert.Equal(10, result.Value?.Count);
    }
}