using DigitalFamilyCookbook.Handlers.Commands.System;

namespace DigitalFamilyCookbook.Tests.Handlers.Commands.System;

public class SaveSiteSettingsTests
{
    [Fact]
    public async Task ItSuccessfullySavesSiteSettings()
    {
        var siteSettings = MockSiteSettings.GenerateSiteSettings();
        var siteSettingsApiModel = SiteSettingsApiModel.FromDomainModel(siteSettings);

        var systemRepostiory = new Mock<ISystemRepository>();
        systemRepostiory.Setup(s => s.SaveSiteSettings(It.IsAny<SiteSettings>()));

        var command = new SaveSiteSettings.Command
        {
            Title = MockDataGenerator.RandomString(12),
            IsPublic = MockDataGenerator.RandomBoolean(),
            AllowPublicRegistration = MockDataGenerator.RandomBoolean(),
        };

        var handler = new SaveSiteSettings.Handler(systemRepostiory.Object);

        var result = await handler.Handle(command, new CancellationToken());

        Assert.Equal(Unit.Value, result);
    }
}