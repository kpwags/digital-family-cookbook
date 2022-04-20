namespace DigitalFamilyCookbook.Data.Domain.Models;

public class Meat
{
    public string Id { get; set; } = string.Empty;

    public int MeatId { get; set; }

    public string Name { get; set; } = string.Empty;

    public IEnumerable<RecipeMeat> RecipeMeats { get; set; } = Enumerable.Empty<RecipeMeat>();

    public static Meat None() => new Meat();

    public static Meat FromDto(MeatDto dto)
    {
        return new Meat
        {
            Id = dto.Id,
            MeatId = dto.MeatId,
            Name = dto.Name,
            RecipeMeats = dto.RecipeMeats.Select(rm => RecipeMeat.FromDto(rm)),
        };
    }
}
