namespace DigitalFamilyCookbook.Data.Dtos;

public class RecipeCategoryDto : BaseDto
{
    public string Id { get; set; } = string.Empty;

    public int RecipeCategoryId { get; set; }

    public int RecipeId { get; set; }

    public RecipeDto Recipe { get; set; } = RecipeDto.None();

    public int CategoryId { get; set; }

    public CategoryDto Category { get; set; } = CategoryDto.None();
}
