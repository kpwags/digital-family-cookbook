namespace DigitalFamilyCookbook.Data.Repositories;

public interface ISystemRepository
{
    SiteSettings GetSiteSettings(int siteSettingsId);

    Task SaveSiteSettings(SiteSettings settings);

    Task<SiteSettings> RegnerateInvitationCode();
}