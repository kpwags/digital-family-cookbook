using DigitalFamilyCookbook.Handlers.Commands.Recipes;

namespace DigitalFamilyCookbook.Tests.Handlers.Commands.Recipes;

public class MarkRecipeAsFavoritesTests
{
    private readonly Mock<IRecipeRepository> _recipeRepository;

    public MarkRecipeAsFavoritesTests()
    {
        _recipeRepository = new();
    }

    [Fact]
    public async Task ItSuccessfullyMarksARecipeAsAFavorite()
    {
        var command = new MarkRecipeAsFavorite.Command
        {
            UserAccountId = Guid.NewGuid().ToString(),
            RecipeId = MockDataGenerator.RandomInteger(),
        };

        var handler = new MarkRecipeAsFavorite.Handler(_recipeRepository.Object);

        var result = await handler.Handle(command, new CancellationToken());

        Assert.Equal(Unit.Value, result);
    }
}