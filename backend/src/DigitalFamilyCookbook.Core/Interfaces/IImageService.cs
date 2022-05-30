using System.Drawing;

namespace DigitalFamilyCookbook.Core.Interfaces;

public interface IImageService
{
    Task<byte[]> ResizeImage(Stream stream, ImageType type, int? desiredWidth = null, int? desiredHeight = null);

    Task SaveImage(byte[] img, ImageType type, string path);

    Task SaveImage(Stream stream, ImageType type, string path);

    string ConvertToBase64(byte[] img);

    string ConvertToBase64(string filepath);
}