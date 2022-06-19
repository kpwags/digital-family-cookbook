using DigitalFamilyCookbook.Handlers.Queries.Recipes;
using Microsoft.AspNetCore.Http;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.Recipes;

public class GetUserRecipesTests
{
    private Mock<IRecipeRepository> _recipeRepository;

    public GetUserRecipesTests()
    {
        _recipeRepository = new Mock<IRecipeRepository>();
    }

    [Fact]
    public async Task ItReturnsAUsersRecipesWhenTheIdIsPassedInTheQuery()
    {
        var recipes = MockRecipe.GenerateDomainModelList(10);

        _recipeRepository
            .Setup(r => r.GetUserRecipes(It.IsAny<string>()))
            .Returns(recipes);

        var query = new GetUserRecipes.Query { UserAccountId = MockDataGenerator.RandomId() };

        var handler = new GetUserRecipes.Handler(_recipeRepository.Object, Mock.Of<IHttpContextAccessor>());

        var result = await handler.Handle(query, new CancellationToken());

        Assert.True(result.IsSuccessful);
        Assert.Equal(10, result.Value?.Count);
    }

    [Fact]
    public async Task ItReturnsAUsersRecipesWhenTheIdIsPulledFromHttpContext()
    {
        var userAccount = MockUser.GenerateUserApiModel();
        var recipes = MockRecipe.GenerateDomainModelList(10);

        _recipeRepository
            .Setup(r => r.GetUserRecipes(userAccount.Id))
            .Returns(recipes);

        var httpContextAccessor = MockSession.BuildSession(userAccount);

        var query = new GetUserRecipes.Query();

        var handler = new GetUserRecipes.Handler(_recipeRepository.Object, httpContextAccessor.Object);

        var result = await handler.Handle(query, new CancellationToken());

        Assert.True(result.IsSuccessful);
        Assert.Equal(10, result.Value?.Count);
    }
}