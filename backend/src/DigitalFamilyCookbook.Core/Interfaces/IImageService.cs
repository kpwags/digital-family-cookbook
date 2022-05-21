using System.Drawing;

namespace DigitalFamilyCookbook.Core.Interfaces;

public interface IImageService
{
    public Image Resize(Image src, int newWidth, int maxHeight);

    public Image ResizeAndRatio(Image src, int newWidth, int newHeight);
}