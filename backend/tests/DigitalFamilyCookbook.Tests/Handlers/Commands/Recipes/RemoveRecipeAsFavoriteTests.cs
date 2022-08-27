using DigitalFamilyCookbook.Handlers.Commands.Recipes;

namespace DigitalFamilyCookbook.Tests.Handlers.Commands.Recipes;

public class RemoveRecipeAsFavoriteTests
{
    private readonly Mock<IRecipeRepository> _recipeRepository;

    public RemoveRecipeAsFavoriteTests()
    {
        _recipeRepository = new();
    }

    [Fact]
    public async Task ItSuccessfullyMarksARecipeAsAFavorite()
    {
        var command = new RemoveRecipeAsFavorite.Command
        {
            UserAccountId = Guid.NewGuid().ToString(),
            RecipeId = MockDataGenerator.RandomInteger(),
        };

        var handler = new RemoveRecipeAsFavorite.Handler(_recipeRepository.Object);

        var result = await handler.Handle(command, new CancellationToken());

        Assert.Equal(Unit.Value, result);
    }
}