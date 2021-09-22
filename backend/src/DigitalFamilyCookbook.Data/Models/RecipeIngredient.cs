namespace DigitalFamilyCookbook.Data.Models
{
    public class RecipeIngredient
    {
        public int RecipeIngredientId { get; set; }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; } = Recipe.None();

        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; } = Ingredient.None();
    }
}