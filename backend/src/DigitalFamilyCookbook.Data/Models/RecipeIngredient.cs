namespace DigitalFamilyCookbook.Data.Models
{
    public class RecipeIngredient
    {
        public string Id { get; set; } = string.Empty;

        public int RecipeIngredientId { get; set; }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; } = Recipe.None();

        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; } = Ingredient.None();
    }
}