namespace DigitalFamilyCookbook.Data.Domain.Models;

public class Recipe : BaseDomainModel
{
    public string Id { get; set; } = string.Empty;

    public int RecipeId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public bool IsPublic { get; set; }

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

    public IEnumerable<RecipeMeat> RecipeMeats { get; set; } = Enumerable.Empty<RecipeMeat>();

    public IEnumerable<RecipeCategory> RecipeCategories { get; set; } = Enumerable.Empty<RecipeCategory>();

    public IEnumerable<RecipeIngredient> RecipeIngredients { get; set; } = Enumerable.Empty<RecipeIngredient>();

    public IEnumerable<RecipeStep> RecipeSteps { get; set; } = Enumerable.Empty<RecipeStep>();

    public IEnumerable<RecipeNote> RecipeNotes { get; set; } = Enumerable.Empty<RecipeNote>();

    public UserAccount UserAccount { get; set; } = UserAccount.None();

    public static Recipe None() => new Recipe();

    public static Recipe FromDto(RecipeDto dto)
    {
        return new Recipe
        {

        };
    }
}
