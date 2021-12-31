namespace DigitalFamilyCookbook.Data.Models
{
    public class RecipeMeat
    {
        public string Id { get; set; } = string.Empty;

        public int RecipeMeatId { get; set; }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; } = Recipe.None();

        public int MeatId { get; set; }

        public Meat Meat { get; set; } = Meat.None();
    }
}