namespace DigitalFamilyCookbook.Data.Dtos;

public class CategoryDto : BaseDto
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<RecipeCategoryDto> RecipeCategories { get; set; } = new List<RecipeCategoryDto>();

    public static CategoryDto None() => new CategoryDto();
}
