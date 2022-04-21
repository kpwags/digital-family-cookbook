namespace DigitalFamilyCookbook.Data.Domain.Models;

public class RecipeStep : BaseDomainModel
{
    public string Id { get; set; } = string.Empty;

    public int RecipeStepId { get; set; }

    public int RecipeId { get; set; }

    public Recipe Recipe { get; set; } = Recipe.None();

    public int StepId { get; set; }

    public Step Step { get; set; } = Step.None();

    public static RecipeStep None() => new RecipeStep();

    public static RecipeStep FromDto(RecipeStepDto dto)
    {
        return new RecipeStep
        {
            Id = dto.Id,
            RecipeStepId = dto.RecipeStepId,
            Recipe = Recipe.FromDto(dto.Recipe),
            StepId = dto.StepId,
            Step = Step.FromDto(dto.Step),
            DateCreated = dto.DateCreated,
            DateUpdated = dto.DateUpdated,
        };
    }
}
