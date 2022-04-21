namespace DigitalFamilyCookbook.Data.Domain.Models;

public class RecipeCategory : BaseDomainModel
{
    public string Id { get; set; } = string.Empty;

    public int RecipeCategoryId { get; set; }

    public int RecipeId { get; set; }

    public Recipe Recipe { get; set; } = Recipe.None();

    public int CategoryId { get; set; }

    public Category Category { get; set; } = Category.None();

    public static RecipeCategory None() => new RecipeCategory();

    public static RecipeCategory FromDto(RecipeCategoryDto dto)
    {
        return new RecipeCategory
        {
            Id = dto.Id,
            RecipeCategoryId = dto.RecipeCategoryId,
            RecipeId = dto.RecipeId,
            Recipe = Recipe.FromDto(dto.Recipe),
            CategoryId = dto.CategoryId,
            Category = Category.FromDto(dto.Category),
            DateCreated = dto.DateCreated,
            DateUpdated = dto.DateUpdated,
        };
    }
}
