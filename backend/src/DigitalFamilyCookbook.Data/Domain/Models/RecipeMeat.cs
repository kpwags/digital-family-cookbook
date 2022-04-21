namespace DigitalFamilyCookbook.Data.Domain.Models;

public class RecipeMeat : BaseDomainModel
{
    public string Id { get; set; } = string.Empty;

    public int RecipeMeatId { get; set; }

    public int RecipeId { get; set; }

    public Recipe Recipe { get; set; } = Recipe.None();

    public int MeatId { get; set; }

    public Meat Meat { get; set; } = Meat.None();

    public static RecipeMeat None() => new RecipeMeat();

    public static RecipeMeat FromDto(RecipeMeatDto dto)
    {
        return new RecipeMeat
        {
            Id = dto.Id,
            RecipeMeatId = dto.RecipeMeatId,
            RecipeId = dto.RecipeId,
            Recipe = Recipe.FromDto(dto.Recipe),
            MeatId = dto.MeatId,
            Meat = Meat.FromDto(dto.Meat),
            DateCreated = dto.DateCreated,
            DateUpdated = dto.DateUpdated,
        };
    }
}
