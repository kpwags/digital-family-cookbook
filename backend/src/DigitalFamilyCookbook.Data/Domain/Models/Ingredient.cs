namespace DigitalFamilyCookbook.Data.Domain.Models;

public class Ingredient : BaseDomainModel
{
    public string Id { get; set; } = string.Empty;

    public int IngredientId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int? SortOrder { get; set; }

    public IEnumerable<RecipeIngredient> RecipeIngredients { get; set; } = Enumerable.Empty<RecipeIngredient>();

    public Recipe Recipe { get; set; } = Recipe.None();

    public static Ingredient None() => new Ingredient();

    public static Ingredient FromDto(IngredientDto dto)
    {
        return new Ingredient
        {
            Id = dto.Id,
            IngredientId = dto.IngredientId,
            Name = dto.Name,
            SortOrder = dto.SortOrder,
            RecipeIngredients = dto.RecipeIngredients.Select(ri => RecipeIngredient.FromDto(ri)),
            Recipe = Recipe.FromDto(dto.Recipe),
            DateCreated = dto.DateCreated,
            DateUpdated = dto.DateUpdated,
        };
    }
}
