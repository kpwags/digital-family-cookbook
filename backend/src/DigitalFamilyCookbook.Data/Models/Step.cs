namespace DigitalFamilyCookbook.Data.Models;

public class Step
{
    public string Id { get; set; } = string.Empty;

    public int StepId { get; set; }

    public string Direction { get; set; } = string.Empty;

    public int SortOrder { get; set; }

    public IEnumerable<RecipeStep> RecipeSteps { get; set; } = Enumerable.Empty<RecipeStep>();

    public Recipe Recipe { get; set; } = Recipe.None();

    public static Step None() => new Step();
}
