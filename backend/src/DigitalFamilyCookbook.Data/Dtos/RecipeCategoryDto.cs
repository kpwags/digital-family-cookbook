namespace DigitalFamilyCookbook.Data.Dtos;

public class RecipeCategoryDto : BaseDto
{
    public int RecipeCategoryId { get; set; }

    public int RecipeId { get; set; }

    public RecipeDto Recipe { get; set; } = RecipeDto.None();

    public int CategoryId { get; set; }

    public CategoryDto Category { get; set; } = CategoryDto.None();
}
