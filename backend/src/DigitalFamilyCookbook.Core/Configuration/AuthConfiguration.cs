namespace DigitalFamilyCookbook.Core.Configuration;

public class AuthConfiguration
{
    public string JwtSecret { get; set; } = string.Empty;

    public int JwtLifespan { get; set; }
}
