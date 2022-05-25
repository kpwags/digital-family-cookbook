using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
namespace DigitalFamilyCookbook.Core.Services;

public class ImageService : IImageService
{
    public async Task<byte[]> ResizeImage(Stream stream, int? desiredWidth = null, int? desiredHeight = null)
    {
        using var image = Image.Load(stream, new JpegDecoder());

        var aspectRatio = decimal.Parse(image.Width.ToString()) / decimal.Parse(image.Height.ToString());
        int width;
        int height;

        if (desiredWidth is not null)
        {
            width = desiredWidth.Value;
            height = (int)(desiredWidth.Value / aspectRatio);
        }
        else if (desiredHeight is not null)
        {
            height = desiredHeight.Value;
            width = (int)(desiredHeight.Value * aspectRatio);
        }
        else
        {
            throw new Exception("Width or height was not provided");
        }

        image.Mutate(i => i.Resize(width, height));

        using var ms = new MemoryStream();

        await image.SaveAsync(ms, new JpegEncoder());

        return ms.ToArray();
    }

    public async Task SaveImage(byte[] img, string path)
    {
        using var image = Image.Load(img, new JpegDecoder());

        await image.SaveAsJpegAsync(path);
    }

    public async Task SaveImage(Stream stream, string path)
    {
        using var image = Image.Load(stream, new JpegDecoder());

        await image.SaveAsJpegAsync(path);
    }

    public string ConvertToBase64(byte[] img)
    {
        using var image = Image.Load(img, new JpegDecoder());

        return image.ToBase64String(JpegFormat.Instance);
    }

    public string ConvertToBase64(string path)
    {
        using var image = Image.Load(path, new JpegDecoder());

        return image.ToBase64String(JpegFormat.Instance);
    }
}