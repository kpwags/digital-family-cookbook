namespace DigitalFamilyCookbook.Data.Domain.Models;

public class Category
{
    public string Id { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public Recipe Recipe { get; set; } = Recipe.None();

    public IEnumerable<RecipeCategory> RecipeCategories { get; set; } = Enumerable.Empty<RecipeCategory>();

    public static Category None() => new Category();

    public static Category FromDto(CategoryDto dto)
    {
        return new Category
        {
            Id = dto.Id,
            CategoryId = dto.CategoryId,
            Name = dto.Name,
            Recipe = Recipe.FromDto(dto.Recipe),
            RecipeCategories = dto.RecipeCategories.Select(rc => RecipeCategory.FromDto(rc)),
        };
    }
}
