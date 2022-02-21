namespace DigitalFamilyCookbook.Tests.Utilities.Mocks;

public static class MockSiteSettings
{
    public static SiteSettings GenerateNonPublicSiteSettings()
    {
        return new SiteSettings
        {
            Id = Guid.NewGuid().ToString(),
            SiteSettingsId = 1,
            Title = "Digital Family Cookbook",
            IsPublic = false,
            AllowPublicRegistration = false,
            InvitationCode = Guid.NewGuid().ToString(),
        };
    }

    public static SiteSettings GeneratePublicSiteSettings()
    {
        return new SiteSettings
        {
            Id = Guid.NewGuid().ToString(),
            SiteSettingsId = 1,
            Title = "Digital Family Cookbook",
            IsPublic = true,
            AllowPublicRegistration = true,
            InvitationCode = Guid.NewGuid().ToString(),
        };
    }
}