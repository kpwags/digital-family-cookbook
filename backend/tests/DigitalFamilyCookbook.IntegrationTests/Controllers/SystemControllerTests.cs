using DigitalFamilyCookbook.Handlers.Commands.System;

namespace DigitalFamilyCookbook.IntegrationTests.Controllers;

public class SystemControllerTests : BaseServerTest<BaseWebApplicationFactory>
{
    public SystemControllerTests(BaseWebApplicationFactory factory, ITestOutputHelper output) : base(factory, output) { }

    [Fact]
    public async Task ItRequiresAuthentication()
    {
        var client = CreateClient();

        var response = await client.GetAsync("/system/getroles");

        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task ItSavesANewRole()
    {
        var client = CreateClient(Mocks.User.MockAdmin);

        var payload = PayloadBuilder.Build(new SaveRoleType.Command
        {
            Id = string.Empty,
            Name = MockDataGenerator.RandomString(8, false),
        }); ;

        var response = await client.PostAsync("/system/saverole", payload);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ItUpdatesAnExistingRole()
    {
        var client = CreateClient(Mocks.User.MockAdmin);

        var payload = PayloadBuilder.Build(new SaveRoleType.Command
        {
            Id = "USERROLEID",
            Name = MockDataGenerator.RandomString(8, false),
        });

        var response = await client.PostAsync("/system/saverole", payload);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ItReturnsRoles()
    {
        var client = CreateClient(Mocks.User.MockAdmin);

        var response = await client.GetAsync("/system/getroles");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Contains("ADMINISTRATOR", responseString);
        Assert.Contains("USER", responseString);
    }

    [Fact]
    public async Task ItReturnsTheSpecifiedRole()
    {
        var client = CreateClient(Mocks.User.MockAdmin);

        var response = await client.GetAsync("/system/getrolebyid?id=USERROLEID");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ItErrorsWhenGettingARoleThatDoesNotExist()
    {
        var client = CreateClient(Mocks.User.MockAdmin);

        var response = await client.GetAsync("/system/getrolebyid?id=WTF");

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ItDeletesARole()
    {
        var client = CreateClient(Mocks.User.MockAdmin);

        var payload = PayloadBuilder.Build(new DeleteRoleType.Command
        {
            Id = "USERROLEID",
        });

        var response = await client.PostAsync("/system/deleterole", payload);

        response.EnsureSuccessStatusCode();
    }
}
