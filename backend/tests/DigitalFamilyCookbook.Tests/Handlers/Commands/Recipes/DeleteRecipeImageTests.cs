using DigitalFamilyCookbook.Handlers.Commands.Recipes;

namespace DigitalFamilyCookbook.Tests.Handlers.Commands.Recipes;

public class DeleteRecipeImageTests
{
    private Mock<IFileService> _fileService;
    private Mock<IRecipeRepository> _recipeRepository;

    public DeleteRecipeImageTests()
    {
        _fileService = new Mock<IFileService>();
        _recipeRepository = new Mock<IRecipeRepository>();
    }

    [Fact]
    public async Task ItDeletesAnImageWithoutTouchingTheRecipe()
    {
        _fileService.Setup(f => f.DeleteRecipeImage(It.IsAny<string>()));

        var command = new DeleteRecipeImage.Command
        {
            ImageFilename = "test.jpg",
        };

        var handler = new DeleteRecipeImage.Handler(_fileService.Object, _recipeRepository.Object);

        var result = await handler.Handle(command, new CancellationToken());

        _recipeRepository.Verify(r => r.DeleteRecipeImage(It.IsAny<int>()), Times.Never);

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public async Task ItDeletesAnImageAndUpdatesTheRecipe()
    {
        _fileService.Setup(f => f.DeleteRecipeImage(It.IsAny<string>()));
        _recipeRepository.Setup(r => r.DeleteRecipeImage(It.IsAny<int>()));

        var command = new DeleteRecipeImage.Command
        {
            ImageFilename = "test.jpg",
            RecipeId = 10,
        };

        var handler = new DeleteRecipeImage.Handler(_fileService.Object, _recipeRepository.Object);

        var result = await handler.Handle(command, new CancellationToken());

        _recipeRepository.Verify(r => r.DeleteRecipeImage(It.IsAny<int>()), Times.Once);

        Assert.True(result.IsSuccessful);
    }
}
