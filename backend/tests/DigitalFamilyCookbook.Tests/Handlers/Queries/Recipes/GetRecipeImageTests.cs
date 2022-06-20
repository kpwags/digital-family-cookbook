using DigitalFamilyCookbook.Handlers.Queries.Recipes;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.Recipes;

public class GetRecipeImageTests
{
    private Mock<IFileService> _fileService;

    public GetRecipeImageTests()
    {
        _fileService = new Mock<IFileService>();
    }

    [Fact]
    public async Task ItRetrievesTheBase64EncodedImageString()
    {
        var base64string = Convert.ToBase64String(MockDataGenerator.RandomByteArray(5));

        _fileService
            .Setup(f => f.GetRecipeImage(It.IsAny<string>()))
            .Returns(base64string);

        var query = new GetRecipeImage.Query { Filename = "test.jpg" };
        var handler = new GetRecipeImage.Handler(_fileService.Object);

        var result = await handler.Handle(query, new CancellationToken());

        Assert.True(result.IsSuccessful);
        Assert.Equal(base64string, result.Value);
    }

    [Fact]
    public async Task ItErrorsIfNoImageFoundOrUnableToRead()
    {
        var base64string = Convert.ToBase64String(MockDataGenerator.RandomByteArray(5));

        _fileService
            .Setup(f => f.GetRecipeImage(It.IsAny<string>()))
            .Returns("");

        var query = new GetRecipeImage.Query { Filename = "test.jpg" };
        var handler = new GetRecipeImage.Handler(_fileService.Object);

        var result = await handler.Handle(query, new CancellationToken());

        Assert.False(result.IsSuccessful);
        Assert.Equal("Unable to retrieve image", result.ErrorMessage);
    }
}