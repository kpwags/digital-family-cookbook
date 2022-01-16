namespace DigitalFamilyCookbook.Data.Repositories;

public interface ISystemRepository
{
    SiteSettings GetSiteSettings(int siteSettingsId);
}