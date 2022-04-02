namespace DigitalFamilyCookbook.ApiModels;

public class CategoryApiModel
{
    public string Id { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    // public Recipe Recipe { get; set; } = Recipe.None();

    // public IEnumerable<RecipeCategory> RecipeCategories { get; set; } = Enumerable.Empty<RecipeCategory>();

    public static CategoryApiModel None() => new CategoryApiModel();

    public static CategoryApiModel FromDomainModel(Category cateogry)
    {
        return new CategoryApiModel
        {
            Id = cateogry.Id,
            CategoryId = cateogry.CategoryId,
            Name = cateogry.Name,
            // Recipe = Recipe.FromDto(cateogry.Recipe),
            // RecipeCategories = cateogry.RecipeCategories.Select(rc => RecipeCategory.FromDto(rc)),
        };
    }
}
