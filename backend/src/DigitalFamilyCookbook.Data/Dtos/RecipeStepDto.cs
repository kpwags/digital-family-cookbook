namespace DigitalFamilyCookbook.Data.Dtos;

public class RecipeStepDto
{
    public string Id { get; set; } = string.Empty;

    public int RecipeStepId { get; set; }

    public int RecipeId { get; set; }

    public RecipeDto Recipe { get; set; } = RecipeDto.None();

    public int StepId { get; set; }

    public StepDto Step { get; set; } = StepDto.None();
}
