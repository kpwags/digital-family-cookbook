namespace DigitalFamilyCookbook.Data.Dtos;

public class RecipeMeatDto : BaseDto
{
    public int RecipeMeatId { get; set; }

    public int RecipeId { get; set; }

    public RecipeDto Recipe { get; set; } = RecipeDto.None();

    public int MeatId { get; set; }

    public MeatDto Meat { get; set; } = MeatDto.None();
}
