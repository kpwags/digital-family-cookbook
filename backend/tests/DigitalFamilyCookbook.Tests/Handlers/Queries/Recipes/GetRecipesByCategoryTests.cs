using DigitalFamilyCookbook.Handlers.Queries.Recipes;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.Recipes;

public class GetRecipesByCategoryTests
{
    private Mock<IRecipeRepository> _recipeRepository;
    private Mock<ICategoryRepository> _categoryRepository;
    private Mock<IFileService> _fileService;

    public GetRecipesByCategoryTests()
    {
        _recipeRepository = new Mock<IRecipeRepository>();
        _categoryRepository = new Mock<ICategoryRepository>();
        _fileService = new Mock<IFileService>();
    }

    [Fact]
    public async Task ItReturnsRecipesForAGivenCategory()
    {
        var recipes = MockRecipe.GenerateDomainModelList(10);

        _recipeRepository
            .Setup(r => r.GetRecipesForCategory(It.IsAny<int>()))
            .Returns(recipes);

        _categoryRepository
            .Setup(c => c.Get(It.IsAny<int>()))
            .Returns(MockCategory.GenerateCategory());

        var query = new GetRecipesByCategory.Query
        {
            CategoryId = 1,
            IncludeImages = false,
        };

        var handler = new GetRecipesByCategory.Handler(_recipeRepository.Object, _categoryRepository.Object, _fileService.Object);

        var result = await handler.Handle(query, new CancellationToken());

        Assert.True(result.IsSuccessful);
        Assert.Equal(10, result.Value?.Recipes.Count);

        // ensure with include images set to false, this is not called
        _fileService.Verify(f => f.GetRecipeImage(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task ItReturnsRecipesForAGivenCategoryWithImages()
    {
        var recipes = MockRecipe.GenerateDomainModelList(10);

        _recipeRepository
            .Setup(r => r.GetRecipesForCategory(It.IsAny<int>()))
            .Returns(recipes);

        _categoryRepository
            .Setup(c => c.Get(It.IsAny<int>()))
            .Returns(MockCategory.GenerateCategory());

        _fileService
            .Setup(f => f.GetRecipeImage(It.IsAny<string>()))
            .Returns(MockDataGenerator.RandomString(100));

        var query = new GetRecipesByCategory.Query
        {
            CategoryId = 1,
            IncludeImages = true,
        };

        var handler = new GetRecipesByCategory.Handler(_recipeRepository.Object, _categoryRepository.Object, _fileService.Object);

        var result = await handler.Handle(query, new CancellationToken());

        Assert.True(result.IsSuccessful);
        Assert.Equal(10, result.Value?.Recipes.Count);

        // ensure with include images set to false, this is not called
        _fileService.Verify(f => f.GetRecipeImage(It.IsAny<string>()), Times.Exactly(20));
    }
}