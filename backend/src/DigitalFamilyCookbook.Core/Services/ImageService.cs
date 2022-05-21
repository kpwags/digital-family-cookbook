using System.Drawing;
using System.Drawing.Imaging;

namespace DigitalFamilyCookbook.Core.Services;

public class ImageService : IImageService
{
    public Image Resize(Image src, int newWidth, int maxHeight)
    {
        src.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
        src.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

        int newHeight = src.Height * newWidth / src.Width;
        if (newHeight > maxHeight) // Height resize if necessary
        {
            //newWidth = FullSizeImage.Width * maxHeight / FullSizeImage.Height;
            newHeight = maxHeight;
        }
        newHeight = maxHeight;

        // Create the new image with the sizes we've calculated
        var newImage = src.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);

        return newImage;
    }

    public Image ResizeAndRatio(Image src, int newWidth, int newHeight)
    {
        var templateWidth = newWidth;
        var templateHeight = newHeight;
        var templateRate = double.Parse(templateWidth.ToString()) / templateHeight;
        var initRate = double.Parse(src.Width.ToString()) / src.Height;

        if (templateRate == initRate)
        {
            var templateImage = new Bitmap(templateWidth, templateHeight);
            var templateG = Graphics.FromImage(templateImage);

            templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            templateG.Clear(Color.White);
            templateG.DrawImage(src, new System.Drawing.Rectangle(0, 0, templateWidth, templateHeight), new System.Drawing.Rectangle(0, 0, src.Width, src.Height), System.Drawing.GraphicsUnit.Pixel);

            return templateImage;
        }
        else
        {
            Image pickedImage;
            Graphics pickedG;

            Rectangle fromR = new Rectangle(0, 0, 0, 0);
            Rectangle toR = new Rectangle(0, 0, 0, 0);

            if (templateRate > initRate)
            {
                pickedImage = new Bitmap(src.Width, int.Parse(Math.Floor(src.Width / templateRate).ToString()));
                pickedG = Graphics.FromImage(pickedImage);

                fromR.X = 0;
                fromR.Y = int.Parse(Math.Floor((src.Height - src.Width / templateRate) / 2).ToString());
                fromR.Width = src.Width;
                fromR.Height = int.Parse(Math.Floor(src.Width / templateRate).ToString());

                toR.X = 0;
                toR.Y = 0;
                toR.Width = src.Width;
                toR.Height = int.Parse(Math.Floor(src.Width / templateRate).ToString());
            }
            else
            {
                pickedImage = new System.Drawing.Bitmap(int.Parse(Math.Floor(src.Height * templateRate).ToString()), src.Height);
                pickedG = System.Drawing.Graphics.FromImage(pickedImage);

                fromR.X = int.Parse(Math.Floor((src.Width - src.Height * templateRate) / 2).ToString());
                fromR.Y = 0;
                fromR.Width = int.Parse(Math.Floor(src.Height * templateRate).ToString());
                fromR.Height = src.Height;

                toR.X = 0;
                toR.Y = 0;
                toR.Width = int.Parse(Math.Floor(src.Height * templateRate).ToString());
                toR.Height = src.Height;
            }

            pickedG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            pickedG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            pickedG.DrawImage(src, toR, fromR, System.Drawing.GraphicsUnit.Pixel);

            var templateImage = new Bitmap(templateWidth, templateHeight);
            var templateG = Graphics.FromImage(templateImage);

            templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            templateG.Clear(Color.White);
            templateG.DrawImage(pickedImage, new System.Drawing.Rectangle(0, 0, templateWidth, templateHeight), new System.Drawing.Rectangle(0, 0, pickedImage.Width, pickedImage.Height), System.Drawing.GraphicsUnit.Pixel);

            templateG.Dispose();
            pickedG.Dispose();
            pickedImage.Dispose();

            return templateImage;
        }
    }
}