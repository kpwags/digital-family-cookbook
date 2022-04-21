namespace DigitalFamilyCookbook.Data.Domain.Models;

public class Category : BaseDomainModel
{
    public string Id { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public IEnumerable<RecipeCategory> RecipeCategories { get; set; } = Enumerable.Empty<RecipeCategory>();

    public static Category None() => new Category();

    public static Category FromDto(CategoryDto dto)
    {
        return new Category
        {
            Id = dto.Id,
            CategoryId = dto.CategoryId,
            Name = dto.Name,
            RecipeCategories = dto.RecipeCategories.Select(rc => RecipeCategory.FromDto(rc)),
            DateCreated = dto.DateCreated,
            DateUpdated = dto.DateUpdated,
        };
    }
}
