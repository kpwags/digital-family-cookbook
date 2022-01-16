namespace DigitalFamilyCookbook.Data.Domain.Models;

public class Step
{
    public string Id { get; set; } = string.Empty;

    public int StepId { get; set; }

    public string Direction { get; set; } = string.Empty;

    public int SortOrder { get; set; }

    public IEnumerable<RecipeStep> RecipeSteps { get; set; } = Enumerable.Empty<RecipeStep>();

    public Recipe Recipe { get; set; } = Recipe.None();

    public static Step None() => new Step();

    public static Step FromDto(StepDto dto)
    {
        return new Step
        {
            Id = dto.Id,
            StepId = dto.StepId,
            Direction = dto.Direction,
            SortOrder = dto.SortOrder,
            RecipeSteps = dto.RecipeSteps.Select(rs => RecipeStep.FromDto(rs)),
            Recipe = Recipe.FromDto(dto.Recipe),
        };
    }
}
