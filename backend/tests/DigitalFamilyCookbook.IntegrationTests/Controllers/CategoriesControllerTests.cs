using DigitalFamilyCookbook.Handlers.Commands.Categories;

namespace DigitalFamilyCookbook.IntegrationTests.Controllers;

public class CategoriesControllerTests
{
    [Fact]
    public async Task ItRetrievesAllCategories()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var response = await client.GetAsync("/categories/getall");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var categories = ResponseReader.Read<IReadOnlyCollection<CategoryApiModel>>(responseString);

        Assert.Equal(5, categories?.Count);
    }

    [Fact]
    public async Task ItRetrievesASpecificCategory()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var response = await client.GetAsync("/categories/get?id=1");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var category = ResponseReader.Read<CategoryApiModel>(responseString);

        Assert.Equal("Italian", category?.Name);
    }

    [Fact]
    public async Task ItErrorsIfTheCategoryDoesNotExist()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var response = await client.GetAsync("/categories/get?id=100");

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ItCreatesACategory()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var payload = PayloadBuilder.Build(new CreateCategory.Command
        {
            Name = "Salads",
        });

        var response = await client.PostAsync("/categories/create", payload);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ItWillNotCreateADuplicateCategory()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var payload = PayloadBuilder.Build(new CreateCategory.Command
        {
            Name = "Grilled",
        });

        var response = await client.PostAsync("/categories/create", payload);

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ItUpdatesACategory()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var payload = PayloadBuilder.Build(new UpdateCategory.Command
        {
            Id = 5,
            Name = "Chinese",
        });

        var response = await client.PatchAsync("/categories/update", payload);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ItWillErrorUpdatingIfCategoryNotFound()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var payload = PayloadBuilder.Build(new UpdateCategory.Command
        {
            Id = 9,
            Name = "Salmon",
        });

        var response = await client.PatchAsync("/categories/update", payload);

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ItWillNotUpdateCategoryIfADuplicateWouldBeCreated()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var payload = PayloadBuilder.Build(new UpdateCategory.Command
        {
            Id = 5,
            Name = "Mexican",
        });

        var response = await client.PatchAsync("/categories/update", payload);

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ItDeletesACategory()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var response = await client.DeleteAsync("/categories/delete?id=3");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ItWillErrorDeletingIfCategoryNotFound()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var response = await client.DeleteAsync("/categories/delete?id=22");

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }
}