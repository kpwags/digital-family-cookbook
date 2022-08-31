namespace DigitalFamilyCookbook.Data.Dtos;

public class RecipeDto : BaseDto
{
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

    public decimal? Calories { get; set; }

    public decimal? Carbohydrates { get; set; }

    public decimal? Sugar { get; set; }

    public decimal? Fat { get; set; }

    public decimal? Protein { get; set; }

    public decimal? Fiber { get; set; }

    public decimal? Cholesterol { get; set; }

    public List<RecipeMeatDto> RecipeMeats { get; set; } = new List<RecipeMeatDto>();

    public List<RecipeCategoryDto> RecipeCategories { get; set; } = new List<RecipeCategoryDto>();

    public List<IngredientDto> Ingredients { get; set; } = new List<IngredientDto>();

    public List<StepDto> Steps { get; set; } = new List<StepDto>();

    public List<RecipeNoteDto> RecipeNotes { get; set; } = new List<RecipeNoteDto>();

    public List<RecipeFavoriteDto> RecipeFavorites { get; set; } = new List<RecipeFavoriteDto>();

    public string UserAccountId { get; set; } = string.Empty;

    public UserAccountDto UserAccount { get; set; } = UserAccountDto.None();

    public static RecipeDto None() => new RecipeDto();
}
