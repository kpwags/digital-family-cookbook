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

    public async Task SaveSiteSettings(SiteSettings settings)
    {
        var siteSettings = _db.SiteSettings.Where(s => s.SiteSettingsId == 1).FirstOrDefault();

        if (siteSettings == null)
        {
            throw new Exception("Unable to find site settings");
        }

        siteSettings.AllowPublicRegistration = settings.AllowPublicRegistration;
        siteSettings.IsPublic = settings.IsPublic;
        siteSettings.Title = settings.Title;
        siteSettings.LandingPageText = settings.LandingPageText;

        _db.Update(siteSettings);

        await _db.SaveChangesAsync();
    }

    public async Task<SiteSettings> RegnerateInvitationCode()
    {
        var settings = _db.SiteSettings.Where(s => s.SiteSettingsId == 1).FirstOrDefault();

        if (settings == null)
        {
            throw new Exception("Unable to find site settings");
        }

        settings.InvitationCode = Guid.NewGuid().ToString();

        _db.Update(settings);

        await _db.SaveChangesAsync();

        return SiteSettings.FromDto(settings);
    }
}