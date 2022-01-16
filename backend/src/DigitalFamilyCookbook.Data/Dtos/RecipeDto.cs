namespace DigitalFamilyCookbook.Data.Dtos;

public class RecipeDto
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

    public IEnumerable<MeatDto> Meats { get; set; } = Enumerable.Empty<MeatDto>();

    public IEnumerable<RecipeMeatDto> RecipeMeats { get; set; } = Enumerable.Empty<RecipeMeatDto>();

    public IEnumerable<CategoryDto> Categories { get; set; } = Enumerable.Empty<CategoryDto>();

    public IEnumerable<RecipeCategoryDto> RecipeCategories { get; set; } = Enumerable.Empty<RecipeCategoryDto>();

    public IEnumerable<IngredientDto> Ingredients { get; set; } = Enumerable.Empty<IngredientDto>();

    public IEnumerable<RecipeIngredientDto> RecipeIngredients { get; set; } = Enumerable.Empty<RecipeIngredientDto>();

    public IEnumerable<StepDto> Steps { get; set; } = Enumerable.Empty<StepDto>();

    public IEnumerable<RecipeStepDto> RecipeSteps { get; set; } = Enumerable.Empty<RecipeStepDto>();

    public UserAccountDto UserAccount { get; set; } = UserAccountDto.None();

    public static RecipeDto None() => new RecipeDto();
}
