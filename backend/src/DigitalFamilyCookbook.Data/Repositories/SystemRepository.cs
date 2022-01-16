namespace DigitalFamilyCookbook.Data.Repositories;

public class SystemRepository : ISystemRepository
{
    private readonly ApplicationDbContext _db;

    public SystemRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public SiteSettings GetSiteSettings(int siteSettingsId)
    {
        var settings = _db.SiteSettings.Where(s => s.SiteSettingsId == siteSettingsId).FirstOrDefault();

        if (settings == null)
        {
            return SiteSettings.None();
        }

        return SiteSettings.FromDto(settings);
    }
}