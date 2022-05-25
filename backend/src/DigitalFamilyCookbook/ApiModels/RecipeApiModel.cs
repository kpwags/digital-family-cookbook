namespace DigitalFamilyCookbook.ApiModels;

public class RecipeApiModel : BaseApiModel
{
    public string Id { get; set; } = string.Empty;

    public int RecipeId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public bool IsPublic { get; set; }

    public int Servings { get; set; }

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

    public IReadOnlyCollection<MeatApiModel> Meats { get; set; } = Array.Empty<MeatApiModel>();

    public IReadOnlyCollection<CategoryApiModel> Categories { get; set; } = Array.Empty<CategoryApiModel>();

    public IReadOnlyCollection<IngredientApiModel> Ingredients { get; set; } = Array.Empty<IngredientApiModel>();

    public IReadOnlyCollection<StepApiModel> Steps { get; set; } = Array.Empty<StepApiModel>();

    // Todo: integrate notes
    // public IReadOnlyCollection<Note> Notes { get; set; } = Array.Empty<Note>();

    public string UserAccountId { get; set; } = string.Empty;

    public UserAccountApiModel UserAccount { get; set; } = UserAccountApiModel.None();

    public static RecipeApiModel None() => new RecipeApiModel();

    public static RecipeApiModel FromDomainModel(Recipe recipe) => new RecipeApiModel
    {
        RecipeId = recipe.RecipeId,
        Id = recipe.Id,
        Name = recipe.Name,
        Description = recipe.Description,
        IsPublic = recipe.IsPublic,
        Servings = recipe.Servings,
        Source = recipe.Source,
        SourceUrl = recipe.SourceUrl,
        Time = recipe.Time,
        ActiveTime = recipe.ActiveTime,
        ImageUrl = recipe.ImageUrl,
        ImageUrlLarge = recipe.ImageUrlLarge,
        Calories = recipe.Calories,
        Carbohydrates = recipe.Carbohydrates,
        Sugar = recipe.Sugar,
        Fat = recipe.Fat,
        Protein = recipe.Protein,
        Fiber = recipe.Fiber,
        Cholesterol = recipe.Cholesterol,
        Categories = recipe.Categories.Select(rc => CategoryApiModel.FromDomainModel(rc)).ToList(),
        Meats = recipe.Meats.Select(rm => MeatApiModel.FromDomainModel(rm)).ToList(),
        Ingredients = recipe.Ingredients.Select(ri => IngredientApiModel.FromDomainModel(ri)).ToList(),
        Steps = recipe.Steps.Select(rs => StepApiModel.FromDomainModel(rs)).ToList(),
        UserAccount = UserAccountApiModel.FromDomainModel(recipe.UserAccount),
        UserAccountId = recipe.UserAccountId,
    };

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        var model = obj as RecipeApiModel;

        if (model is null)
        {
            return false;
        }

        return this.Equals(model);
    }

    public bool Equals(RecipeApiModel model)
    {
        if (model is null)
        {
            return false;
        }

        if (Object.ReferenceEquals(this, model))
        {
            return true;
        }

        if (this.GetType() != model.GetType())
        {
            return false;
        }

        return Id == model.Id
            && RecipeId == model.RecipeId
            && Name == model.Name;
    }

    public override int GetHashCode() => (Id, RecipeId, Name, IsPublic).GetHashCode();
}
