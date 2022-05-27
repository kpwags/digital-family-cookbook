using Microsoft.AspNetCore.Http;

namespace DigitalFamilyCookbook.Core.Interfaces;

public interface IFileService
{
    Task<(string Filename, byte[] Image, byte[] LargeImage)> SaveRecipeImage(IFormFile image);

    void DeleteRecipeImage(string filename);

    string GetRecipeImage(string filename);
}