namespace DigitalFamilyCookbook.Data.Dtos;

public class RecipeIngredientDto : BaseDto
{
    public string Id { get; set; } = string.Empty;

    public int RecipeIngredientId { get; set; }

    public int RecipeId { get; set; }

    public RecipeDto Recipe { get; set; } = RecipeDto.None();

    public int IngredientId { get; set; }

    public IngredientDto Ingredient { get; set; } = IngredientDto.None();
}
