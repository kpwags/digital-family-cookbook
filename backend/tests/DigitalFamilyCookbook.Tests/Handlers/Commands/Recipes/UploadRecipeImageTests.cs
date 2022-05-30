using DigitalFamilyCookbook.Handlers.Commands.Recipes;
using Microsoft.AspNetCore.Http;

namespace DigitalFamilyCookbook.Tests.Handlers.Commands.Recipes;

public class UploadRecipeImageTests
{
    private Mock<IFileService> _fileService;

    public UploadRecipeImageTests()
    {
        _fileService = new Mock<IFileService>();
    }

    [Fact]
    public async Task ItFailsGracefullyIfNoImageUploaded()
    {
        var command = new UploadRecipeImage.Command
        {
            Image = null,
        };

        var handler = new UploadRecipeImage.Handler(_fileService.Object);

        var result = await handler.Handle(command, new CancellationToken());

        Assert.False(result.IsSuccessful);
        Assert.Equal("No Image Uploaded", result.ErrorMessage);
    }

    [Fact]
    public async Task ItUploadsAnImageForARecipeSuccessfully()
    {
        var command = new UploadRecipeImage.Command
        {
            Image = MockFile.GenerateFile("image.jpg", "image/jpeg").Object,
        };

        _fileService
            .Setup(f => f.SaveRecipeImage(It.IsAny<IFormFile>()))
            .ReturnsAsync(("image.jpg", MockDataGenerator.RandomString(200), MockDataGenerator.RandomString(200)));

        var handler = new UploadRecipeImage.Handler(_fileService.Object);

        var result = await handler.Handle(command, new CancellationToken());

        Assert.True(result.IsSuccessful);
    }
}

