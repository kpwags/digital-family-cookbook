namespace DigitalFamilyCookbook.Data.Domain.Models;

public class SiteSettings
{
    public int SiteSettingsId { get; set; }

    public string Id { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public bool IsPublic { get; set; }

    public bool AllowPublicRegistration { get; set; }

    public string InvitationCode { get; set; } = string.Empty;

    public static SiteSettings None() => new SiteSettings();

    public static SiteSettings FromDto(SiteSettingsDto dto)
    {
        return new SiteSettings
        {
            SiteSettingsId = dto.SiteSettingsId,
            Id = dto.Id,
            Title = dto.Title,
            IsPublic = dto.IsPublic,
            AllowPublicRegistration = dto.AllowPublicRegistration,
            InvitationCode = dto.InvitationCode,
        };
    }
}