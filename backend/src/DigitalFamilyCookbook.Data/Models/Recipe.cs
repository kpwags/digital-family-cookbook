using System.Linq;
using System.Collections.Generic;

namespace DigitalFamilyCookbook.Data.Models
{
    public class Recipe
    {
        public string Id { get; set; } = string.Empty;

        public int RecipeId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public bool IsPublic { get; set; }

        public string? Notes { get; set; } = string.Empty;

        public string? Source { get; set; } = string.Empty;

        public string? SourceUrl { get; set; } = string.Empty;

        public int? Time { get; set; }

        public int? ActiveTime { get; set; }

        public string? ImageUrl { get; set; }

        public string? ImageUrlLarge { get; set; }

        public decimal? Calories { get; set; }

        public decimal? Carbohydrates { get; set; }

        public decimal? Sugar { get; set; }

        public decimal? Fat { get; set; }

        public decimal? Protein { get; set; }

        public decimal? Fiber { get; set; }

        public decimal? Cholesterol { get; set; }

        public IEnumerable<Meat> Meats { get; set; } = Enumerable.Empty<Meat>();

        public IEnumerable<RecipeMeat> RecipeMeats { get; set; } = Enumerable.Empty<RecipeMeat>();

        public IEnumerable<Category> Categories { get; set; } = Enumerable.Empty<Category>();

        public IEnumerable<RecipeCategory> RecipeCategories { get; set; } = Enumerable.Empty<RecipeCategory>();

        public IEnumerable<Ingredient> Ingredients { get; set; } = Enumerable.Empty<Ingredient>();

        public IEnumerable<RecipeIngredient> RecipeIngredients { get; set; } = Enumerable.Empty<RecipeIngredient>();

        public IEnumerable<Step> Steps { get; set; } = Enumerable.Empty<Step>();

        public IEnumerable<RecipeStep> RecipeSteps { get; set; } = Enumerable.Empty<RecipeStep>();

        public UserAccount UserAccount { get; set; } = UserAccount.None();

        public static Recipe None() => new Recipe();
    }
}