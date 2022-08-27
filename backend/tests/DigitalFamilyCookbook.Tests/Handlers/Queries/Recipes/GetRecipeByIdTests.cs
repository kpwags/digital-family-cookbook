using DigitalFamilyCookbook.Handlers.Queries.Recipes;
using Microsoft.AspNetCore.Http;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.Recipes;

public class GetRecipeByIdTests
{
    private readonly Mock<IRecipeRepository> _recipeRepository;
    private readonly Mock<ICategoryRepository> _categoryRepository;
    private readonly Mock<IMeatRepository> _meatRepository;
    private readonly Mock<IFileService> _fileService;

    public GetRecipeByIdTests()
    {
        _recipeRepository = new Mock<IRecipeRepository>();
        _categoryRepository = new Mock<ICategoryRepository>();
        _meatRepository = new Mock<IMeatRepository>();
        _fileService = new Mock<IFileService>();
    }

    [Fact]
    public async Task ItReturnsTheRecipeIfItExists()
    {
        var userAccount = MockUser.GenerateUserApiModel();

        var recipe = MockRecipe.GenerateDomainModel();
        recipe.ImageUrl = string.Empty;

        var apiModel = RecipeApiModel.FromDomainModel(recipe);

        _recipeRepository
            .Setup(r => r.GetById(recipe.RecipeId))
            .Returns(recipe);

        _categoryRepository
            .Setup(r => r.GetForRecipe(recipe.RecipeId))
            .Returns(MockCategory.GenerateCategoryList(2));

        _meatRepository
            .Setup(r => r.GetForRecipe(recipe.RecipeId))
            .Returns(MockMeat.GenerateMeatList(1));

        var httpContextAccessor = MockSession.BuildSession(userAccount);

        var query = new GetRecipeById.Query { Id = recipe.RecipeId };

        var handler = new GetRecipeById.Handler(_recipeRepository.Object, _categoryRepository.Object, _meatRepository.Object, _fileService.Object, httpContextAccessor.Object);

        var result = await handler.Handle(query, new CancellationToken());

        Assert.True(result.IsSuccessful);
        Assert.Equal(apiModel, result.Value);
    }

    [Fact]
    public async Task ItReturnsAnErrorIfTheRecipeIsNotFound()
    {
        var userAccount = MockUser.GenerateUserApiModel();

        _recipeRepository
            .Setup(r => r.GetById(It.IsAny<int>()))
            .Throws(new Exception("Recipe not found"));

        var httpContextAccessor = MockSession.BuildSession(userAccount);

        var query = new GetRecipeById.Query { Id = MockDataGenerator.RandomInteger() };

        var handler = new GetRecipeById.Handler(_recipeRepository.Object, _categoryRepository.Object, _meatRepository.Object, _fileService.Object, httpContextAccessor.Object);

        var result = await handler.Handle(query, new CancellationToken());

        Assert.False(result.IsSuccessful);
        Assert.Equal("Recipe not found", result.ErrorMessage);
    }
}