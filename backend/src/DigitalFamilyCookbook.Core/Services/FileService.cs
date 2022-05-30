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

    public async Task<(string Filename, string Image, string LargeImage)> SaveRecipeImage(IFormFile image)
    {
        var file = new FileInfo(image.FileName);

        var imageType = file.Extension.ToLower() switch
        {
            ".jpg" => ImageType.Jpeg,
            ".jpeg" => ImageType.Jpeg,
            ".png" => ImageType.Png,
            ".gif" => ImageType.Gif,
            _ => throw new Exception("Unknown image type")
        };

        var rootFilename = Guid.NewGuid().ToString();

        var filename = Path.Combine(_hostEnvironment.ContentRootPath, $"{_uploadDirectoriesConfiguration.Recipe}{rootFilename}_sm.jpg");
        var largeFilename = Path.Combine(_hostEnvironment.ContentRootPath, $"{_uploadDirectoriesConfiguration.Recipe}{rootFilename}.jpg");

        using var memoryStream = new MemoryStream();

        await image.CopyToAsync(memoryStream);

        memoryStream.Seek(0, SeekOrigin.Begin);

        await _imageService.SaveImage(memoryStream, imageType, largeFilename);

        memoryStream.Seek(0, SeekOrigin.Begin);

        var resizedImage = await _imageService.ResizeImage(memoryStream, imageType, 400);

        await _imageService.SaveImage(resizedImage, ImageType.Jpeg, filename);

        return (rootFilename, GetRecipeImage($"{rootFilename}_sm.jpg"), GetRecipeImage($"{rootFilename}.jpg"));
    }

    public string GetRecipeImage(string filename)
    {
        var imageFilename = $"{_uploadDirectoriesConfiguration.Recipe}{filename}";

        return _imageService.ConvertToBase64(imageFilename);
    }

    public void DeleteRecipeImage(string filename)
    {
        var thumbnailPath = Path.Combine(_hostEnvironment.ContentRootPath, $"{_uploadDirectoriesConfiguration.Recipe}{filename}_sm.jpg");
        var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, $"{_uploadDirectoriesConfiguration.Recipe}{filename}.jpg");

        if (File.Exists(thumbnailPath))
        {
            File.Delete(thumbnailPath);
        }

        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }
    }
}