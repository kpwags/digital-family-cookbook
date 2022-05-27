namespace DigitalFamilyCookbook.Tests.Utilities.Mocks;

public static class MockIngredient
{
    public static IngredientDto GenerateDto(int? sortOrder = null) => new IngredientDto
    {
        IngredientId = MockDataGenerator.RandomInteger(),
        Id = MockDataGenerator.RandomId(),
        Name = MockDataGenerator.RandomString(20),
        SortOrder = sortOrder.HasValue ? sortOrder.Value : MockDataGenerator.RandomInteger(),
    };

    public static Ingredient GenerateDomainModel(int? sortOrder = null) => Ingredient.FromDto(GenerateDto(sortOrder));

    public static IngredientApiModel GenerateApiModel(int? sortOrder = null) => IngredientApiModel.FromDomainModel(GenerateDomainModel(sortOrder));

    public static List<IngredientDto> GenerateForRecipeDto(int ingredientCount = 5)
    {
        var ingredients = new List<IngredientDto>();

        for (var i = 0; i < ingredientCount; i += 1)
        {
            ingredients.Add(GenerateDto(i + 1));
        }

        return ingredients;
    }

    public static List<Ingredient> GenerateForRecipe(int ingredientCount = 5)
    {
        var ingredients = new List<Ingredient>();

        for (var i = 0; i < ingredientCount; i += 1)
        {
            ingredients.Add(GenerateDomainModel(i + 1));
        }

        return ingredients;
    }

    public static List<IngredientApiModel> GenerateForRecipeApiModel(int ingredientCount = 5)
    {
        var ingredients = new List<IngredientApiModel>();

        for (var i = 0; i < ingredientCount; i += 1)
        {
            ingredients.Add(GenerateApiModel(i + 1));
        }

        return ingredients;
    }
}