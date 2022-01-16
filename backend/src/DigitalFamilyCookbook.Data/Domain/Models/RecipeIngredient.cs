namespace DigitalFamilyCookbook.Data.Domain.Models;

public class RecipeIngredient
{
    public string Id { get; set; } = string.Empty;

    public int RecipeIngredientId { get; set; }

    public int RecipeId { get; set; }

    public Recipe Recipe { get; set; } = Recipe.None();

    public int IngredientId { get; set; }

    public Ingredient Ingredient { get; set; } = Ingredient.None();

    public static RecipeIngredient None() => new RecipeIngredient();

    public static RecipeIngredient FromDto(RecipeIngredientDto dto)
    {
        return new RecipeIngredient
        {
            Id = dto.Id,
            RecipeIngredientId = dto.RecipeIngredientId,
            RecipeId = dto.RecipeId,
            Recipe = Recipe.FromDto(dto.Recipe),
            IngredientId = dto.IngredientId,
            Ingredient = Ingredient.FromDto(dto.Ingredient),
        };
    }
}
