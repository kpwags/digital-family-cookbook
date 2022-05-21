using Microsoft.AspNetCore.Http;

namespace DigitalFamilyCookbook.Core.Interfaces;

public interface IFileService
{
    Task<(string filename, byte[] image, byte[] largeImage)> SaveRecipeImage(IFormFile image);
}