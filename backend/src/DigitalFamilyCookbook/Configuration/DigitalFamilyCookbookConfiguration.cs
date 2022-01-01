namespace DigitalFamilyCookbook.Configuration;

public class DigitalFamilyCookbookConfiguration
{
    public string JwtSecret { get; set; } = string.Empty;

    public int JwtLifespan { get; set; }
}
