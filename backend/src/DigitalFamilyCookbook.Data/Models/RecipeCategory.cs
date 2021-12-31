namespace DigitalFamilyCookbook.Data.Models;

public class RecipeCategory
{
    public string Id { get; set; } = string.Empty;

    public int RecipeCategoryId { get; set; }

    public int RecipeId { get; set; }

    public Recipe Recipe { get; set; } = Recipe.None();

    public int CategoryId { get; set; }

    public Category Category { get; set; } = Category.None();
}
