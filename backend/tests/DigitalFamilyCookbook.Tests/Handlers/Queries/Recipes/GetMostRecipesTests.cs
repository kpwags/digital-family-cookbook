using DigitalFamilyCookbook.Handlers.Queries.Recipes;
using Microsoft.AspNetCore.Http;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.Recipes;

public class GetMostRecipesTests
{
    private readonly Mock<IRecipeRepository> _recipeRepository;
    private readonly Mock<IFileService> _fileService;
    private readonly Mock<IHttpContextAccessor> _httpContextAccessor;

    public GetMostRecipesTests()
    {
        _recipeRepository = new();
        _fileService = new();
        _httpContextAccessor = new();
    }

    [Fact]
    public async Task ItRetrievesTheMostRecentRecipes()
    {
        var recipes = MockRecipe.GenerateDomainModelList(10);

        _recipeRepository
            .Setup(r => r.GetRecentRecipes(10, It.IsAny<bool>()))
            .Returns(recipes);

        var query = new GetRecentRecipes.Query
        {
            Count = 10,
            IncludeImages = false,
        };

        var handler = new GetRecentRecipes.Handler(_recipeRepository.Object, _fileService.Object, _httpContextAccessor.Object);

        var result = await handler.Handle(query, new CancellationToken());

        Assert.Equal(10, result.Count);
    }
}