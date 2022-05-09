namespace DigitalFamilyCookbook.Data.Dtos;

public class MeatDto : BaseDto
{
    public string Id { get; set; } = string.Empty;

    public int MeatId { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<RecipeMeatDto> RecipeMeats { get; set; } = new List<RecipeMeatDto>();

    public static MeatDto None() => new MeatDto();
}
