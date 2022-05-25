using DigitalFamilyCookbook.Core.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace DigitalFamilyCookbook.Core.Services;

public class FileService : IFileService
{
    private readonly UploadDirectoriesConfiguration _uploadDirectoriesConfiguration;
    private readonly IImageService _imageService;
    private readonly IHostEnvironment _hostEnvironment;

    public FileService(DigitalFamilyCookbookConfiguration configuration, IImageService imageService, IHostEnvironment hostEnvironment)
    {
        _uploadDirectoriesConfiguration = configuration.UploadDirectories;
        _imageService = imageService;
        _hostEnvironment = hostEnvironment;
    }

    public async Task<(string Filename, byte[] Image, byte[] LargeImage)> SaveRecipeImage(IFormFile image)
    {
        var rootFilename = Guid.NewGuid().ToString();

        var filename = Path.Combine(_hostEnvironment.ContentRootPath, $"{_uploadDirectoriesConfiguration.Recipe}{rootFilename}_sm.jpg");
        var largeFilename = Path.Combine(_hostEnvironment.ContentRootPath, $"{_uploadDirectoriesConfiguration.Recipe}{rootFilename}.jpg");

        using var memoryStream = new MemoryStream();

        await image.CopyToAsync(memoryStream);

        var originalImage = new byte[memoryStream.Length];
        memoryStream.Read(originalImage, 0, (int)memoryStream.Length);

        memoryStream.Seek(0, SeekOrigin.Begin);

        await _imageService.SaveImage(memoryStream, largeFilename);

        memoryStream.Seek(0, SeekOrigin.Begin);

        var resizedImage = await _imageService.ResizeImage(memoryStream, 400);

        await _imageService.SaveImage(resizedImage, filename);

        return (rootFilename, resizedImage, originalImage);
    }

    public string GetRecipeImage(string filename)
    {
        var imageFilename = $"{_uploadDirectoriesConfiguration.Recipe}{filename}";

        return _imageService.ConvertToBase64(imageFilename);
    }
}