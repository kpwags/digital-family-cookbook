using DigitalFamilyCookbook.Handlers.Queries.Recipes;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.Recipes;

public class GetRecipesByMeatTests
{
    private Mock<IRecipeRepository> _recipeRepository;
    private Mock<IMeatRepository> _meatRepository;
    private Mock<IFileService> _fileService;

    public GetRecipesByMeatTests()
    {
        _recipeRepository = new Mock<IRecipeRepository>();
        _meatRepository = new Mock<IMeatRepository>();
        _fileService = new Mock<IFileService>();
    }

    [Fact]
    public async Task ItReturnsRecipesForAGivenMeat()
    {
        var recipes = MockRecipe.GenerateDomainModelList(10);

        _recipeRepository
            .Setup(r => r.GetRecipesForMeat(It.IsAny<int>()))
            .Returns(recipes);

        _meatRepository
            .Setup(c => c.Get(It.IsAny<int>()))
            .Returns(MockMeat.GenerateMeat());

        var query = new GetRecipesByMeat.Query
        {
            MeatId = 1,
            IncludeImages = false,
        };

        var handler = new GetRecipesByMeat.Handler(_recipeRepository.Object, _meatRepository.Object, _fileService.Object);

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
            .Setup(r => r.GetRecipesForMeat(It.IsAny<int>()))
            .Returns(recipes);

        _meatRepository
            .Setup(c => c.Get(It.IsAny<int>()))
            .Returns(MockMeat.GenerateMeat());

        _fileService
            .Setup(f => f.GetRecipeImage(It.IsAny<string>()))
            .Returns(MockDataGenerator.RandomString(100));

        var query = new GetRecipesByMeat.Query
        {
            MeatId = 1,
            IncludeImages = true,
        };

        var handler = new GetRecipesByMeat.Handler(_recipeRepository.Object, _meatRepository.Object, _fileService.Object);

        var result = await handler.Handle(query, new CancellationToken());

        Assert.True(result.IsSuccessful);
        Assert.Equal(10, result.Value?.Recipes.Count);

        // ensure with include images set to false, this is not called
        _fileService.Verify(f => f.GetRecipeImage(It.IsAny<string>()), Times.Exactly(20));
    }
}