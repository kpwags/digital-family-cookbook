using DigitalFamilyCookbook.Handlers.Commands.Meats;

namespace DigitalFamilyCookbook.IntegrationTests.Controllers;

public class MeatsControllerTests
{
    [Fact]
    public async Task ItRetrievesAllMeats()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var response = await client.GetAsync("/meats/getall");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var meats = ResponseReader.Read<IReadOnlyCollection<MeatApiModel>>(responseString);

        Assert.Equal(5, meats?.Count);
    }

    [Fact]
    public async Task ItRetrievesASpecificMeat()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var response = await client.GetAsync("/meats/get?id=1");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var meat = ResponseReader.Read<MeatApiModel>(responseString);

        Assert.Equal("Beef", meat?.Name);
    }

    [Fact]
    public async Task ItErrorsIfTheMeatDoesNotExist()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var response = await client.GetAsync("/meats/get?id=100");

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ItCreatesAMeat()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var payload = PayloadBuilder.Build(new CreateMeat.Command
        {
            Name = "Tofu",
        });

        var response = await client.PostAsync("/meats/create", payload);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ItWillNotCreateADuplicateMeat()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var payload = PayloadBuilder.Build(new CreateMeat.Command
        {
            Name = "Beef",
        });

        var response = await client.PostAsync("/meats/create", payload);

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ItUpdatesAMeat()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var payload = PayloadBuilder.Build(new UpdateMeat.Command
        {
            Id = 5,
            Name = "Vegan",
        });

        var response = await client.PatchAsync("/meats/update", payload);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ItWillErrorUpdatingIfMeatNotFound()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var payload = PayloadBuilder.Build(new UpdateMeat.Command
        {
            Id = 9,
            Name = "Salmon",
        });

        var response = await client.PatchAsync("/meats/update", payload);

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ItWillNotUpdateIfADuplicateWouldBeCreated()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var payload = PayloadBuilder.Build(new UpdateMeat.Command
        {
            Id = 5,
            Name = "Beef",
        });

        var response = await client.PatchAsync("/meats/update", payload);

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ItDeletesAMeat()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var response = await client.DeleteAsync("/meats/delete?id=3");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ItWillErrorDeletingIfMeatNotFound()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var response = await client.DeleteAsync("/meats/delete?id=22");

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }
}