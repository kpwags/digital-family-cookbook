using System.Drawing;

namespace DigitalFamilyCookbook.Core.Interfaces;

public interface IImageService
{
    Task<byte[]> ResizeImage(Stream stream, int? desiredWidth = null, int? desiredHeight = null);

    Task SaveImage(byte[] img, string path);

    Task SaveImage(Stream stream, string path);

    string ConvertToBase64(byte[] img);

    string ConvertToBase64(string filepath);
}