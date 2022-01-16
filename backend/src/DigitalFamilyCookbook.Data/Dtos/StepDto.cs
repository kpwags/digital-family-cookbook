namespace DigitalFamilyCookbook.Data.Dtos;

public class StepDto
{
    public string Id { get; set; } = string.Empty;

    public int StepId { get; set; }

    public string Direction { get; set; } = string.Empty;

    public int SortOrder { get; set; }

    public IEnumerable<RecipeStepDto> RecipeSteps { get; set; } = Enumerable.Empty<RecipeStepDto>();

    public RecipeDto Recipe { get; set; } = RecipeDto.None();

    public static StepDto None() => new StepDto();
}
