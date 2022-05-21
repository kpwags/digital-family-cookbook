using DigitalFamilyCookbook.Core.Configuration;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Drawing.Imaging;
using System;
using System.IO;

namespace DigitalFamilyCookbook.Core.Services;

public class FileService : IFileService
{
    private readonly UploadDirectoriesConfiguration _uploadDirectoriesConfiguration;
    private readonly IImageService _imageService;

    public FileService(DigitalFamilyCookbookConfiguration configuration, IImageService imageService)
    {
        _uploadDirectoriesConfiguration = configuration.UploadDirectories;
        _imageService = imageService;
    }

    public async Task<(string filename, byte[] image, byte[] largeImage)> SaveRecipeImage(IFormFile image)
    {
        var rootFilename = Guid.NewGuid().ToString();

        var filename = $"{_uploadDirectoriesConfiguration.Recipe}{rootFilename}_sm.jpg";
        var largeFilename = $"{_uploadDirectoriesConfiguration.Recipe}{rootFilename}.jpg";

        using var memoryStream = new MemoryStream();

        await image.CopyToAsync(memoryStream);

        Bitmap img = (Bitmap)Bitmap.FromStream(memoryStream);

        byte[] largeImageData = SaveImageToDisk(img, largeFilename);

        var resizedImage = _imageService.Resize(img, 400, 300);

        byte[] imageData = SaveImageToDisk(resizedImage, largeFilename);

        return (rootFilename, imageData, largeImageData);
    }

    private byte[] SaveImageToDisk(Image image, string destination)
    {
        using var ms = new MemoryStream();

        image.Save(ms, ImageFormat.Jpeg);

        var data = new byte[ms.Length];
        ms.Read(data, 0, (int)ms.Length);

        using var stream = new FileStream(destination, FileMode.Create);

        ms.WriteTo(stream);

        return data;
    }
}