namespace DigitalFamilyCookbook.Tests.Utilities.Mocks;

public static class MockRecipe
{
    public static RecipeDto GenerateDto() => new RecipeDto
    {
        RecipeId = MockDataGenerator.RandomInteger(),
        Id = MockDataGenerator.RandomId(),
        Name = MockDataGenerator.RandomString(12),
        IsPublic = MockDataGenerator.RandomBoolean(),
        Description = MockDataGenerator.RandomString(500),
        Servings = MockDataGenerator.RandomInteger(1, 8),
        Source = MockDataGenerator.RandomString(20),
        SourceUrl = MockDataGenerator.RandomUrl(),
        Time = MockDataGenerator.RandomInteger(20, 60),
        ActiveTime = MockDataGenerator.RandomInteger(10, 30),
        Calories = MockDataGenerator.RandomInteger(50, 500),
        Protein = MockDataGenerator.RandomInteger(1, 25),
        Carbohydrates = MockDataGenerator.RandomInteger(1, 25),
        Fat = MockDataGenerator.RandomInteger(1, 15),
        Fiber = MockDataGenerator.RandomInteger(1, 15),
        Cholesterol = MockDataGenerator.RandomInteger(1, 25),
        Sugar = MockDataGenerator.RandomInteger(1, 15),
        Ingredients = MockIngredient.GenerateForRecipeDto(5),
        Steps = MockStep.GenerateForRecipeDto(5),
        ImageUrl = $"{Guid.NewGuid().ToString()}_sm.jpg",
        ImageUrlLarge = $"{Guid.NewGuid().ToString()}.jpg",
    };

    public static Recipe GenerateDomainModel() => Recipe.FromDto(GenerateDto());

    public static RecipeApiModel GenerateApiModel() => RecipeApiModel.FromDomainModel(GenerateDomainModel());

    public static List<RecipeDto> GenerateDtoList(int count = 5)
    {
        var recipes = new List<RecipeDto>();

        for (var i = 0; i < count; i += 1)
        {
            recipes.Add(GenerateDto());
        }

        return recipes;
    }

    public static List<Recipe> GenerateDomainModelList(int count = 5)
    {
        var recipes = GenerateDtoList(count);

        return recipes.Select(r => Recipe.FromDto(r)).ToList();
    }

    public static List<RecipeApiModel> GenerateApiModelList(int count = 5)
    {
        var recipes = GenerateDomainModelList(count);

        return recipes.Select(r => RecipeApiModel.FromDomainModel(r)).ToList();
    }
}