using DigitalFamilyCookbook.Handlers.Commands.System;

namespace DigitalFamilyCookbook.Tests.Handlers.Commands.System;

public class RefreshInvitationCodeTests
{
    [Fact]
    public async Task ItSuccessfullyRefreshesTheCode()
    {
        var siteSettings = MockSiteSettings.GenerateNonPublicSiteSettings();
        var siteSettingsApiModel = SiteSettingsApiModel.FromDomainModel(siteSettings);

        var systemRepostiory = new Mock<ISystemRepository>();
        systemRepostiory.Setup(s => s.RegnerateInvitationCode()).ReturnsAsync(siteSettings);

        var command = new RefreshInvitationCode.Command();

        var handler = new RefreshInvitationCode.Handler(systemRepostiory.Object);

        var result = await handler.Handle(command, new CancellationToken());

        Assert.NotNull(result.Value);

        if (result.Value is not null)
        {
            Assert.True(siteSettingsApiModel.Equals(result.Value));
        }
    }
}