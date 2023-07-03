using DigitalFamilyCookbook.Handlers.Queries.Recipes;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.Recipes;

public class GetRecipesForAdminTests
{
    private Mock<IRecipeRepository> _recipeRepository;

    public GetRecipesForAdminTests()
    {
        _recipeRepository = new Mock<IRecipeRepository>();
    }

    [Fact]
    public async Task ItReturnsAllRecipes()
    {
        var recipes = MockRecipe.GenerateDomainModelList(10);

        _recipeRepository
            .Setup(r => r.GetAll(It.IsAny<bool>()))
            .Returns(recipes);

        var query = new GetRecipesForAdmin.Query();

        var handler = new GetRecipesForAdmin.Handler(_recipeRepository.Object);

        var result = await handler.Handle(query, new CancellationToken());

        Assert.True(result.IsSuccessful);
        Assert.Equal(10, result.Value?.Count);
    }
}