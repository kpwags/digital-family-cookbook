namespace DigitalFamilyCookbook.Data.Dtos;

public class SiteSettingsDto
{
    public int SiteSettingsId { get; set; }

    public string Id { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public bool IsPublic { get; set; }

    public static SiteSettingsDto None() => new SiteSettingsDto();
}