using Microsoft.AspNetCore.Http;

namespace DigitalFamilyCookbook.Core.Interfaces;

public interface IFileService
{
    Task<(string Filename, string Image, string LargeImage)> SaveRecipeImage(IFormFile image);

    void DeleteRecipeImage(string filename);

    string GetRecipeImage(string filename);
}