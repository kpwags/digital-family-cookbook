using DigitalFamilyCookbook.Handlers.Commands.Recipes;

namespace DigitalFamilyCookbook.Tests.Handlers.Commands.Recipes;

public class DeleteRecipeTests
{
    private Mock<IRecipeRepository> _recipeRepository;

    public DeleteRecipeTests()
    {
        _recipeRepository = new Mock<IRecipeRepository>();
    }

    [Fact]
    public async Task ItDeletesARecipe()
    {
        _recipeRepository.Setup(r => r.Delete(It.IsAny<int>()));

        var command = new DeleteRecipe.Command
        {
            Id = 1,
        };

        var handler = new DeleteRecipe.Handler(_recipeRepository.Object);

        var result = await handler.Handle(command, new CancellationToken());

        _recipeRepository.Verify(r => r.Delete(1), Times.Once);

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public async Task ItErrorsAndAlertsTheUserTheRecipeWasNotFound()
    {
        _recipeRepository
            .Setup(r => r.Delete(It.IsAny<int>()))
            .Throws(new Exception("Recipe not found"));

        var command = new DeleteRecipe.Command
        {
            Id = 1,
        };

        var handler = new DeleteRecipe.Handler(_recipeRepository.Object);

        var result = await handler.Handle(command, new CancellationToken());

        Assert.False(result.IsSuccessful);
        Assert.Equal("Recipe not found", result.ErrorMessage);
    }
}
