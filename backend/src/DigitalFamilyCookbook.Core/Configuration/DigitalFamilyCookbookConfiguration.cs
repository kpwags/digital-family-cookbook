namespace DigitalFamilyCookbook.Core.Configuration;

public class DigitalFamilyCookbookConfiguration
{
    public AuthConfiguration Auth { get; set; } = new AuthConfiguration();

    public List<string> CorsAllowedOrigins { get; set; } = new List<string>();
}
