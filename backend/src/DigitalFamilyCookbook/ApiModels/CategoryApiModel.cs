namespace DigitalFamilyCookbook.ApiModels;

public class CategoryApiModel : BaseApiModel
{
    public string Id { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public static CategoryApiModel None() => new CategoryApiModel();

    public static CategoryApiModel FromDomainModel(Category cateogry)
    {
        return new CategoryApiModel
        {
            Id = cateogry.Id,
            CategoryId = cateogry.CategoryId,
            Name = cateogry.Name,
        };
    }
}
