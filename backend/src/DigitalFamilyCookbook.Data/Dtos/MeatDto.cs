namespace DigitalFamilyCookbook.Data.Dtos;

public class MeatDto : BaseDto
{
    public string Id { get; set; } = string.Empty;

    public int MeatId { get; set; }

    public string Name { get; set; } = string.Empty;

    public IEnumerable<RecipeMeatDto> RecipeMeats { get; set; } = Enumerable.Empty<RecipeMeatDto>();

    public static MeatDto None() => new MeatDto();
}
