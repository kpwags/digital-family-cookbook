namespace DigitalFamilyCookbook.Data.Domain.Models;

public class Recipe : BaseDomainModel
{
    public string Id { get; set; } = string.Empty;

    public int RecipeId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public bool IsPublic { get; set; }

    public int Servings { get; set; }

    public string Source { get; set; } = string.Empty;

    public string SourceUrl { get; set; } = string.Empty;

    public int? Time { get; set; }

    public int? ActiveTime { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public string ImageUrlLarge { get; set; } = string.Empty;

    public string ImageData { get; set; } = string.Empty;

    public string LargeImageData { get; set; } = string.Empty;

    public decimal? Calories { get; set; }

    public decimal? Carbohydrates { get; set; }

    public decimal? Sugar { get; set; }

    public decimal? Fat { get; set; }

    public decimal? Protein { get; set; }

    public decimal? Fiber { get; set; }

    public decimal? Cholesterol { get; set; }

    public IEnumerable<Meat> Meats { get; set; } = Enumerable.Empty<Meat>();

    public IEnumerable<Category> Categories { get; set; } = Enumerable.Empty<Category>();

    public IEnumerable<Ingredient> Ingredients { get; set; } = Enumerable.Empty<Ingredient>();

    public IEnumerable<Step> Steps { get; set; } = Enumerable.Empty<Step>();

    public IEnumerable<Note> Notes { get; set; } = Enumerable.Empty<Note>();

    public string UserAccountId { get; set; } = string.Empty;

    public UserAccount UserAccount { get; set; } = UserAccount.None();

    public static Recipe None() => new Recipe();

    public static Recipe FromDto(RecipeDto dto) => new Recipe
    {
        RecipeId = dto.RecipeId,
        Id = dto.Id,
        Name = dto.Name,
        Description = dto.Description,
        IsPublic = dto.IsPublic,
        Servings = dto.Servings,
        Source = dto.Source,
        SourceUrl = dto.SourceUrl,
        Time = dto.Time,
        ActiveTime = dto.ActiveTime,
        ImageUrl = dto.ImageUrl,
        ImageUrlLarge = dto.ImageUrlLarge,
        Calories = dto.Calories,
        Carbohydrates = dto.Carbohydrates,
        Sugar = dto.Sugar,
        Fat = dto.Fat,
        Protein = dto.Protein,
        Fiber = dto.Fiber,
        Cholesterol = dto.Cholesterol,
        Categories = dto.RecipeCategories.Select(rc => Category.FromDto(rc.Category)),
        Meats = dto.RecipeMeats.Select(rm => Meat.FromDto(rm.Meat)),
        Ingredients = dto.Ingredients.Select(i => Ingredient.FromDto(i)),
        Steps = dto.Steps.Select(s => Step.FromDto(s)),
        UserAccountId = dto.UserAccountId,
        UserAccount = UserAccount.FromDto(dto.UserAccount),
    };
}
