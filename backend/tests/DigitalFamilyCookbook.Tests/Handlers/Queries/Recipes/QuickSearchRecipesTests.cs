using DigitalFamilyCookbook.Handlers.Queries.Recipes;
using Microsoft.AspNetCore.Http;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.Recipes;

public class QuickSearchRecipesTests
{
    private readonly Mock<IRecipeRepository> _recipeRepository;
    private readonly Mock<IHttpContextAccessor> _httpContextAccessor;

    public QuickSearchRecipesTests()
    {
        _recipeRepository = new();
        _httpContextAccessor = new();
    }

    [Fact]
    public async Task ItReturnsAListOfRecipes()
    {
        var recipes = MockRecipe.GenerateDomainModelList(10);
        
        _recipeRepository
            .Setup(r => r.QuickSearchRecipes(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
            .Returns(recipes);

        var query = new QuickSearchRecipes.Query()
        {
            Keywords = "test",
            MaxRecipes = 10
        };
        
        var handler = new QuickSearchRecipes.Handler(_recipeRepository.Object, _httpContextAccessor.Object);

        var result = await handler.Handle(query, new CancellationToken());
        
        Assert.Equal(10, result.Count);
    }
}