namespace DigitalFamilyCookbook.Data.Dtos;

public class IngredientDto : BaseDto
{
    public int IngredientId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int? SortOrder { get; set; }

    public int RecipeId { get; set; }

    public RecipeDto Recipe { get; set; } = RecipeDto.None();

    public static IngredientDto None() => new IngredientDto();
}
