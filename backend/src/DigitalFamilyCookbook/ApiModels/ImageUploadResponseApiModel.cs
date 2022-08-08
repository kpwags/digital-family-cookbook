namespace DigitalFamilyCookbook.Models;

public class ImageUploadResponseApiModel : BaseApiModel
{
    public string Filename { get; set; } = string.Empty;

    public string ImageData { get; set; } = string.Empty;

    public string SecondImageData { get; set; } = string.Empty;

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        var model = obj as ImageUploadResponseApiModel;

        if (model is null)
        {
            return false;
        }

        return this.Equals(model);
    }

    public bool Equals(ImageUploadResponseApiModel model)
    {
        if (model is null)
        {
            return false;
        }

        if (Object.ReferenceEquals(this, model))
        {
            return true;
        }

        if (this.GetType() != model.GetType())
        {
            return false;
        }

        return Filename == model.Filename
            && ImageData == model.ImageData
            && SecondImageData == model.SecondImageData;
    }

    public override int GetHashCode() => (Filename, ImageData, SecondImageData).GetHashCode();
}