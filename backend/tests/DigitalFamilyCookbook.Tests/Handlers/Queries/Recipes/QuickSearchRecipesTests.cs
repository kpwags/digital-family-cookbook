using DigitalFamilyCookbook.Handlers.Queries.Recipes;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.Recipes;

public class QuickSearchRecipesTests
{
    private Mock<IRecipeRepository> _recipeRepository;

    public QuickSearchRecipesTests()
    {
        _recipeRepository = new();
    }

    [Fact]
    public async Task ItReturnsAListOfRecipes()
    {
        var recipes = MockRecipe.GenerateDomainModelList(10);
        
        _recipeRepository
            .Setup(r => r.QuickSearchRecipes(It.IsAny<string>(), It.IsAny<int>()))
            .Returns(recipes);

        var query = new QuickSearchRecipes.Query()
        {
            Keywords = "test",
            MaxRecipes = 10
        };
        
        var handler = new QuickSearchRecipes.Handler(_recipeRepository.Object);

        var result = await handler.Handle(query, new CancellationToken());
        
        Assert.Equal(10, result.Count);
    }
}