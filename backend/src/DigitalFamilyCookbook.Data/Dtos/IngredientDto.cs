namespace DigitalFamilyCookbook.Data.Dtos;

public class IngredientDto : BaseDto
{
    public string Id { get; set; } = string.Empty;

    public int IngredientId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int? SortOrder { get; set; }

    public IEnumerable<RecipeIngredientDto> RecipeIngredients { get; set; } = Enumerable.Empty<RecipeIngredientDto>();

    public RecipeDto Recipe { get; set; } = RecipeDto.None();

    public static IngredientDto None() => new IngredientDto();
}
