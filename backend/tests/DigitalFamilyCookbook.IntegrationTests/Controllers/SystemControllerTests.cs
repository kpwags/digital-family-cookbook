using DigitalFamilyCookbook.Handlers.Commands.System;

namespace DigitalFamilyCookbook.IntegrationTests.Controllers;

public class SystemControllerTests
{
    [Fact]
    public async Task ItRequiresAuthentication()
    {
        var client = ApplicationFactory.CreateClient();

        var response = await client.GetAsync("/system/getroles");

        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }

    // TODO: Fix
    // [Fact]
    // public async Task ItBlocksABadToken()
    // {
    //     var client = CreateClient(new UserAccountDto { Id = "NOACCESS", Email = "burglar@crime.com" });

    //     var response = await client.GetAsync("/system/getroles");

    //     Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    // }

    [Fact]
    public async Task ItSavesANewRole()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

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
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

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
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var response = await client.GetAsync("/system/getroles");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Contains("ADMINISTRATOR", responseString);
        Assert.Contains("USER", responseString);
    }

    [Fact]
    public async Task ItReturnsTheSpecifiedRole()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var response = await client.GetAsync("/system/getrolebyid?id=USERROLEID");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ItErrorsWhenGettingARoleThatDoesNotExist()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var response = await client.GetAsync("/system/getrolebyid?id=WTF");

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ItDeletesARole()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var payload = PayloadBuilder.Build(new DeleteRoleType.Command
        {
            Id = "USERROLEID",
        });

        var response = await client.PostAsync("/system/deleterole", payload);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ItSavesSiteSettings()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var payload = PayloadBuilder.Build(new SaveSiteSettings.Command
        {
            Title = "Recipe Manager",
            IsPublic = true,
            AllowPublicRegistration = true,
        });

        var response = await client.PostAsync("/system/savesitesettings", payload);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ItRefreshesTheInvitationCode()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var response = await client.PostAsync("/system/refreshinvitationcode", null);

        response.EnsureSuccessStatusCode();
    }

    // [Fact]
    // public async Task ItGetsUsers()
    // {
    //     var client = CreateClient(Mocks.User.MockAdmin);

    //     var response = await client.GetAsync("/system/getusers?includeRoles=false");

    //     response.EnsureSuccessStatusCode();
    // }

    // [Fact]
    // public async Task ItDeletesAUser()
    // {
    //     var client = CreateClient(Mocks.User.MockAdmin);

    //     var payload = PayloadBuilder.Build(new DeleteUserAccount.Command
    //     {
    //         Id = "KATHRYNJANEWAY74656",
    //     });

    //     var response = await client.PostAsync("/system/deleteuser", payload);

    //     response.EnsureSuccessStatusCode();
    // }


    // Todo: Add User to Role

    // Todo: Remove Role from User
}
