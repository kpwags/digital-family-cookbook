namespace DigitalFamilyCookbook.Core.Configuration;

public class DigitalFamilyCookbookConfiguration
{
    public AuthConfiguration Auth { get; set; } = new AuthConfiguration();

    public List<string> CorsAllowedOrigins { get; set; } = new List<string>();

    public UploadDirectoriesConfiguration UploadDirectories { get; set; } = new UploadDirectoriesConfiguration();
}
