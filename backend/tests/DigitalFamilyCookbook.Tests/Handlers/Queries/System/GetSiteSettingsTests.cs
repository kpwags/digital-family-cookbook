using DigitalFamilyCookbook.Handlers.Queries.System;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.System;

public class GetSiteSettingsTests
{
    [Fact]
    public async Task ItSuccessfullyReturnsSiteSettings()
    {
        var siteSettings = MockSiteSettings.GenerateNonPublicSiteSettings();

        var apiModel = ApiModels.SiteSettingsApiModel.FromDomainModel(siteSettings);

        var systemRepository = new Mock<ISystemRepository>();
        systemRepository.Setup(s => s.GetSiteSettings(1)).Returns(siteSettings);

        var handler = new GetSiteSettings.Handler(systemRepository.Object);

        var result = await handler.Handle(new GetSiteSettings.Query(), new CancellationToken());

        Assert.True(apiModel.Equals(result.Value ?? DigitalFamilyCookbook.ApiModels.SiteSettingsApiModel.None()));
    }
}