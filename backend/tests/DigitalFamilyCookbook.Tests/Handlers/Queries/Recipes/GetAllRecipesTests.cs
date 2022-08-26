using DigitalFamilyCookbook.Handlers.Queries.Recipes;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.Recipes;

public class GetAllRecipesTests
{
    private Mock<IRecipeRepository> _recipeRepository;
    private Mock<IFileService> _fileService;

    public GetAllRecipesTests()
    {
        _recipeRepository = new Mock<IRecipeRepository>();
        _fileService = new Mock<IFileService>();
    }

    [Fact]
    public async Task ItReturnsRecipes()
    {
        var recipes = MockRecipe.GenerateDomainModelList(10);

        _recipeRepository
            .Setup(r => r.GetAllRecipesPaginated(It.IsAny<int>(), It.IsAny<int>()))
            .Returns((recipes, 10));

        var query = new GetAllRecipes.Query
        {
            PageNumber = 1,
            IncludeImages = false,
        };

        var handler = new GetAllRecipes.Handler(_recipeRepository.Object, _fileService.Object);

        var result = await handler.Handle(query, new CancellationToken());

        Assert.True(result.IsSuccessful);
        Assert.Equal(10, result.Value?.Recipes.Count);

        // ensure with include images set to false, this is not called
        _fileService.Verify(f => f.GetRecipeImage(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task ItReturnsRecipesWithImages()
    {
        var recipes = MockRecipe.GenerateDomainModelList(10);

        _recipeRepository
            .Setup(r => r.GetAllRecipesPaginated(It.IsAny<int>(), It.IsAny<int>()))
            .Returns((recipes, 10));

        _fileService
            .Setup(f => f.GetRecipeImage(It.IsAny<string>()))
            .Returns(MockDataGenerator.RandomString(100));

        var query = new GetAllRecipes.Query
        {
            PageNumber = 1,
            IncludeImages = true,
        };

        var handler = new GetAllRecipes.Handler(_recipeRepository.Object, _fileService.Object);

        var result = await handler.Handle(query, new CancellationToken());

        Assert.True(result.IsSuccessful);
        Assert.Equal(10, result.Value?.Recipes.Count);

        // ensure with include images set to false, this is not called
        _fileService.Verify(f => f.GetRecipeImage(It.IsAny<string>()), Times.Exactly(20));
    }
}