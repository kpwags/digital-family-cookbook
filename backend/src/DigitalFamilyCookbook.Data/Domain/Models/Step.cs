namespace DigitalFamilyCookbook.Data.Domain.Models;

public class Step : BaseDomainModel
{
    public string Id { get; set; } = string.Empty;

    public int StepId { get; set; }

    public string Direction { get; set; } = string.Empty;

    public int SortOrder { get; set; }

    public int RecipeId { get; set; }

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
            RecipeId = dto.RecipeId,
            Recipe = Recipe.FromDto(dto.Recipe),
            DateCreated = dto.DateCreated,
            DateUpdated = dto.DateUpdated,
        };
    }
}
