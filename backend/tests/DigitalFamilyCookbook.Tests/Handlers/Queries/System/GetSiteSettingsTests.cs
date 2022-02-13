using DigitalFamilyCookbook.Handlers.Queries.System;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.System;

public class GetSiteSettingsTests
{
    [Fact]
    public async Task ItSuccessfullyReturnsSiteSettings()
    {
        var siteSettings = new SiteSettings
        {
            Id = Guid.NewGuid().ToString(),
            SiteSettingsId = 1,
            Title = "Digital Family Cookbook",
            IsPublic = false,
        };

        var apiModel = ApiModels.SiteSettingsApiModel.FromDomainModel(siteSettings);

        var systemRepository = new Mock<Data.Repositories.ISystemRepository>();
        systemRepository.Setup(s => s.GetSiteSettings(1)).Returns(siteSettings);

        var handler = new GetSiteSettings.Handler(systemRepository.Object);

        var result = await handler.Handle(new GetSiteSettings.Query(), new CancellationToken());

        Assert.True(apiModel.Equals(result.Value));
    }
}