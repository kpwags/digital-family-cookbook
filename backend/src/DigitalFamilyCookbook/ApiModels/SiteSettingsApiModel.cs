namespace DigitalFamilyCookbook.ApiModels;

public class SiteSettingsApiModel
{
    public int SiteSettingsId { get; set; }

    public string Id { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public bool IsPublic { get; set; }

    public static SiteSettingsApiModel None() => new SiteSettingsApiModel();

    public static SiteSettingsApiModel FromDomainModel(SiteSettings settings)
    {
        return new SiteSettingsApiModel
        {
            SiteSettingsId = settings.SiteSettingsId,
            Id = settings.Id,
            Title = settings.Title,
            IsPublic = settings.IsPublic,
        };
    }
}