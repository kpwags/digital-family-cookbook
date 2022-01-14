namespace DigitalFamilyCookbook.Data.Dtos;

public class Category
{
    public string Id { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public Recipe Recipe { get; set; } = Recipe.None();

    public IEnumerable<RecipeCategory> RecipeCategories { get; set; } = Enumerable.Empty<RecipeCategory>();

    public static Category None() => new Category();
}