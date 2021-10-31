using System.Collections.Generic;
using System.Linq;

namespace DigitalFamilyCookbook.Data.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }

        public string Name { get; set; } = string.Empty;

        public int? SortOrder { get; set; }

        public IEnumerable<RecipeIngredient> RecipeIngredients { get; set; } = Enumerable.Empty<RecipeIngredient>();

        public Recipe Recipe { get; set; } = Recipe.None();

        public static Ingredient None() => new Ingredient();
    }
}