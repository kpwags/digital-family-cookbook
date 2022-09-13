using DigitalFamilyCookbook.Handlers.Queries.Recipes;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.Recipes;

public class GetMostRecipesTests
{
    private readonly Mock<IRecipeRepository> _recipeRepository;
    private readonly Mock<IFileService> _fileService;

    public GetMostRecipesTests()
    {
        _recipeRepository = new Mock<IRecipeRepository>();
        _fileService = new Mock<IFileService>();
    }

    [Fact]
    public async Task ItRetrievesTheMostRecentRecipes()
    {
        var recipes = MockRecipe.GenerateDomainModelList(10);

        _recipeRepository
            .Setup(r => r.GetRecentRecipes(10))
            .Returns(recipes);

        var query = new GetRecentRecipes.Query
        {
            Count = 10,
            IncludeImages = false,
        };

        var handler = new GetRecentRecipes.Handler(_recipeRepository.Object, _fileService.Object);

        var result = await handler.Handle(query, new CancellationToken());

        Assert.Equal(10, result.Count);
    }
}