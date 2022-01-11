namespace DigitalFamilyCookbook.Data.Dtos;

public class RecipeStep
{
    public string Id { get; set; } = string.Empty;

    public int RecipeStepId { get; set; }

    public int RecipeId { get; set; }

    public Recipe Recipe { get; set; } = Recipe.None();

    public int StepId { get; set; }

    public Step Step { get; set; } = Step.None();
}
