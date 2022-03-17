using DigitalFamilyCookbook.Core.Configuration;

namespace DigitalFamilyCookbook.IntegrationTests.Controllers;

public class SystemControllerTests : IClassFixture<TestWebApplicationFactory<DigitalFamilyCookbook.Program>>
{
    private readonly TestWebApplicationFactory<DigitalFamilyCookbook.Program> _factory;
    private readonly DigitalFamilyCookbookConfiguration _configuration;

    public SystemControllerTests(TestWebApplicationFactory<DigitalFamilyCookbook.Program> factory, DigitalFamilyCookbookConfiguration configuration)
    {
        _factory = factory;
        _configuration = configuration;
    }

    [Fact]
    public async Task ItReturnsRoles()
    {
        var user = new UserAccountDto
        {
            Id = "JEANLUCPICARD1701D",
            Email = "jeanluc.picard@starfleet.gov"
        };

        var token = MockAuthToken.GenerateToken(user, _configuration.Auth);

        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("Authorization", $"bearer {token}");

        var response = await client.GetAsync("/system/getroles");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Contains("ADMINISTRATOR", responseString);
        Assert.Contains("USER", responseString);
    }
}
