using DigitalFamilyCookbook.Handlers.Queries.Recipes;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.Recipes;

public class GetRecipesByUserTests
{
    private Mock<IRecipeRepository> _recipeRepository;
    private Mock<IUserAccountRepository> _userAccountRepository;
    private Mock<IFileService> _fileService;

    public GetRecipesByUserTests()
    {
        _recipeRepository = new Mock<IRecipeRepository>();
        _userAccountRepository = new Mock<IUserAccountRepository>();
        _fileService = new Mock<IFileService>();
    }

    [Fact]
    public async Task ItReturnsRecipesForAGivenUser()
    {
        var recipes = MockRecipe.GenerateDomainModelList(10);

        _recipeRepository
            .Setup(r => r.GetRecipesForUserPaginated(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .Returns((recipes, 10));

        _userAccountRepository
            .Setup(u => u.GetUserAccountById(It.IsAny<string>(), false))
            .ReturnsAsync(MockUser.GenerateUser());

        var query = new GetRecipesByUser.Query
        {
            UserAccountId = Guid.NewGuid().ToString(),
            IncludeImages = false,
        };

        var handler = new GetRecipesByUser.Handler(_recipeRepository.Object, _userAccountRepository.Object, _fileService.Object);

        var result = await handler.Handle(query, new CancellationToken());

        Assert.True(result.IsSuccessful);
        Assert.Equal(10, result.Value?.Recipes.Count);

        // ensure with include images set to false, this is not called
        _fileService.Verify(f => f.GetRecipeImage(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task ItReturnsRecipesForAGivenUserWithImages()
    {
        var recipes = MockRecipe.GenerateDomainModelList(10);

        _recipeRepository
            .Setup(r => r.GetRecipesForUserPaginated(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .Returns((recipes, 10));

        _userAccountRepository
            .Setup(u => u.GetUserAccountById(It.IsAny<string>(), false))
            .ReturnsAsync(MockUser.GenerateUser());

        _fileService
            .Setup(f => f.GetRecipeImage(It.IsAny<string>()))
            .Returns(MockDataGenerator.RandomString(100));

        var query = new GetRecipesByUser.Query
        {
            UserAccountId = Guid.NewGuid().ToString(),
            IncludeImages = true,
        };

        var handler = new GetRecipesByUser.Handler(_recipeRepository.Object, _userAccountRepository.Object, _fileService.Object);

        var result = await handler.Handle(query, new CancellationToken());

        Assert.True(result.IsSuccessful);
        Assert.Equal(10, result.Value?.Recipes.Count);

        // ensure with include images set to false, this is not called
        _fileService.Verify(f => f.GetRecipeImage(It.IsAny<string>()), Times.Exactly(20));
    }
}