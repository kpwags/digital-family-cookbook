using DigitalFamilyCookbook.Handlers.Queries.Recipes;
using Microsoft.AspNetCore.Http;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.Recipes;

public class GetRecipesBySearchKeywordsTests
{
    private Mock<IRecipeRepository> _recipeRepository;
    private Mock<IFileService> _fileService;
    private Mock<IHttpContextAccessor> _httpContextAccessor;

    public GetRecipesBySearchKeywordsTests()
    {
        _recipeRepository = new();
        _fileService = new();
        _httpContextAccessor = new();
    }

    [Fact]
    public async Task ItReturnsRecipesForAGivenMeat()
    {
        var recipes = MockRecipe.GenerateDomainModelList(10);

        _recipeRepository
            .Setup(r => r.SearchRecipesPaginated(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()))
            .Returns((recipes, 10));

        var query = new GetRecipesBySearchKeywords.Query
        {
            Keywords = "test recipe",
            IncludeImages = false,
        };

        var handler = new GetRecipesBySearchKeywords.Handler(_recipeRepository.Object, _fileService.Object, _httpContextAccessor.Object);

        var result = await handler.Handle(query, new CancellationToken());

        Assert.True(result.IsSuccessful);
        Assert.Equal(10, result.Value?.Recipes.Count);

        // ensure with include images set to false, this is not called
        _fileService.Verify(f => f.GetRecipeImage(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task ItReturnsRecipesForAGivenMeatWithImages()
    {
        var recipes = MockRecipe.GenerateDomainModelList(10);

        _recipeRepository
            .Setup(r => r.SearchRecipesPaginated(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()))
            .Returns((recipes, 10));
        
        _fileService
            .Setup(f => f.GetRecipeImage(It.IsAny<string>()))
            .Returns(MockDataGenerator.RandomString(100));

        var query = new GetRecipesBySearchKeywords.Query
        {
            Keywords = "test recipe",
            IncludeImages = true,
        };

        var handler = new GetRecipesBySearchKeywords.Handler(_recipeRepository.Object, _fileService.Object, _httpContextAccessor.Object);

        var result = await handler.Handle(query, new CancellationToken());

        Assert.True(result.IsSuccessful);
        Assert.Equal(10, result.Value?.Recipes.Count);

        // ensure with include images set to false, this is not called
        _fileService.Verify(f => f.GetRecipeImage(It.IsAny<string>()), Times.Exactly(20));
    }
}