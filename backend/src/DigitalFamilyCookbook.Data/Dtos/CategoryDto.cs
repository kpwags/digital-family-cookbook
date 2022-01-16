namespace DigitalFamilyCookbook.Data.Dtos;

public class CategoryDto
{
    public string Id { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public RecipeDto Recipe { get; set; } = RecipeDto.None();

    public IEnumerable<RecipeCategoryDto> RecipeCategories { get; set; } = Enumerable.Empty<RecipeCategoryDto>();

    public static CategoryDto None() => new CategoryDto();
}
