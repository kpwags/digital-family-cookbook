namespace DigitalFamilyCookbook.IntegrationTests.Controllers;

public class PublicControllerTests
{
    [Fact]
    public async Task ItGetsSiteSettings()
    {
        var client = ApplicationFactory.CreateClient();

        var response = await client.GetAsync("/public/getsitesettings");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var siteSettings = ResponseReader.Read<SiteSettingsApiModel>(responseString);

        Assert.NotNull(siteSettings);
        Assert.Equal("Digital Family Cookbook", siteSettings?.Title);
    }
}