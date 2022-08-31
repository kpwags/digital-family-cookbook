namespace DigitalFamilyCookbook.Data.Dtos;

public class StepDto : BaseDto
{
    public int StepId { get; set; }

    public string Direction { get; set; } = string.Empty;

    public int SortOrder { get; set; }

    public int RecipeId { get; set; }

    public RecipeDto Recipe { get; set; } = RecipeDto.None();

    public static StepDto None() => new StepDto();
}
