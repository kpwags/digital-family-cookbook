using DigitalFamilyCookbook.Handlers.Commands.Recipes;

namespace DigitalFamilyCookbook.IntegrationTests.Controllers;

public class RecipesControllerTests
{
    [Fact]
    public async Task ItGetsARecipeById()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var response = await client.GetAsync("/recipes/get?id=1");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var recipe = ResponseReader.Read<RecipeApiModel>(responseString);

        Assert.NotNull(recipe);
        Assert.Equal("Lime Baked Tilapia", recipe?.Name);
    }

    [Fact]
    public async Task ItGetsAllRecipes()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var response = await client.GetAsync("/recipes/getall");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var recipes = ResponseReader.Read<IReadOnlyCollection<RecipeApiModel>>(responseString);

        Assert.NotNull(recipes);
        Assert.Equal(2, recipes?.Count);
    }

    [Fact]
    public async Task ItRetrievesOnlyTheUsersRecieps()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var response = await client.GetAsync("/recipes/getuserrecipes?userAccountId=BENJAMINSISKO74205");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var recipes = ResponseReader.Read<IReadOnlyCollection<RecipeApiModel>>(responseString);

        Assert.NotNull(recipes);
        Assert.Equal(1, recipes?.Count);
        Assert.Empty(recipes?.Where(r => r.UserAccount.Name != "Ben Sisko"));
    }

    [Fact]
    public async Task ItAddsANewRecipeSuccessfully()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var recipe = MockRecipe.GenerateDomainModel();

        var payload = PayloadBuilder.Build(new CreateRecipe.Command
        {
            ActiveTime = recipe.ActiveTime,
            Calories = recipe.Calories,
            Carbohydrates = recipe.Carbohydrates,
            Cholesterol = recipe.Cholesterol,
            Description = recipe.Description,
            Fat = recipe.Fat,
            Fiber = recipe.Fiber,
            IsPublic = recipe.IsPublic,
            Name = recipe.Name,
            Protein = recipe.Protein,
            Servings = recipe.Servings,
            Source = recipe.Source,
            SourceUrl = recipe.SourceUrl,
            Sugar = recipe.Sugar,
            ImageFilename = recipe.ImageUrlLarge is not null ? recipe.ImageUrlLarge.Replace(".jpg", "") : "",
            Ingredients = recipe.Ingredients.Select(i => new Models.RecipeStep { Name = i.Name, SortOrder = (i.SortOrder.HasValue ? i.SortOrder.Value : 0) }).ToList(),
            Steps = recipe.Steps.Select(d => new Models.RecipeStep { Name = d.Direction, SortOrder = d.SortOrder }).ToList(),
        });

        var response = await client.PostAsync("/recipes/create", payload);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ItWillNotAllowADuplicateRecipeName()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var recipe = MockRecipe.GenerateDomainModel();

        var payload = PayloadBuilder.Build(new CreateRecipe.Command
        {
            ActiveTime = recipe.ActiveTime,
            Calories = recipe.Calories,
            Carbohydrates = recipe.Carbohydrates,
            Cholesterol = recipe.Cholesterol,
            Description = recipe.Description,
            Fat = recipe.Fat,
            Fiber = recipe.Fiber,
            IsPublic = recipe.IsPublic,
            Name = "Lime Baked Tilapia",
            Protein = recipe.Protein,
            Servings = recipe.Servings,
            Source = recipe.Source,
            SourceUrl = recipe.SourceUrl,
            Sugar = recipe.Sugar,
            ImageFilename = recipe.ImageUrlLarge is not null ? recipe.ImageUrlLarge.Replace(".jpg", "") : "",
            Ingredients = recipe.Ingredients.Select(i => new Models.RecipeStep { Name = i.Name, SortOrder = (i.SortOrder.HasValue ? i.SortOrder.Value : 0) }).ToList(),
            Steps = recipe.Steps.Select(d => new Models.RecipeStep { Name = d.Direction, SortOrder = d.SortOrder }).ToList(),
        });

        var response = await client.PostAsync("/recipes/create", payload);

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ItUpdatesANewRecipeSuccessfully()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var recipe = MockRecipe.GenerateDomainModel();

        var payload = PayloadBuilder.Build(new UpdateRecipe.Command
        {
            RecipeId = 2,
            Name = "Peanut Butter and Jelly",
            Description = "<p>An all-time classic</p>",
            Servings = 1,
            Time = 5,
            ActiveTime = 5,
            Calories = 500,
            Protein = 12,
            Carbohydrates = 23,
            Fat = 16,
            Sugar = 10,
            Cholesterol = 76,
            Fiber = 12,
            Ingredients = recipe.Ingredients.Select(i => new Models.RecipeStep { Name = i.Name, SortOrder = (i.SortOrder.HasValue ? i.SortOrder.Value : 0) }).ToList(),
            Steps = recipe.Steps.Select(d => new Models.RecipeStep { Name = d.Direction, SortOrder = d.SortOrder }).ToList(),
        });

        var response = await client.PatchAsync("/recipes/update", payload);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ItWillNotUpdateARecipeToAnExistingRecipeName()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var recipe = MockRecipe.GenerateDomainModel();

        var payload = PayloadBuilder.Build(new UpdateRecipe.Command
        {
            RecipeId = 2,
            Name = "Lime Baked Tilapia",
            Description = "<p>An all-time classic</p>",
            Servings = 1,
            Time = 5,
            ActiveTime = 5,
            Calories = 500,
            Protein = 12,
            Carbohydrates = 23,
            Fat = 16,
            Sugar = 10,
            Cholesterol = 76,
            Fiber = 12,
            Ingredients = recipe.Ingredients.Select(i => new Models.RecipeStep { Name = i.Name, SortOrder = (i.SortOrder.HasValue ? i.SortOrder.Value : 0) }).ToList(),
            Steps = recipe.Steps.Select(d => new Models.RecipeStep { Name = d.Direction, SortOrder = d.SortOrder }).ToList(),
        });

        var response = await client.PatchAsync("/recipes/update", payload);

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ItDeletesARecipe()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var response = await client.DeleteAsync("/recipes/delete?Id=2");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ItWillErrorDeletingIfRecipeNotFound()
    {
        var client = ApplicationFactory.CreateClient(Mocks.User.MockAdmin);

        var response = await client.DeleteAsync("/recipes/delete?id=22");

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }
}