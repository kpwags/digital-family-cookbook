namespace DigitalFamilyCookbook.Tests.Utilities.Mocks;

public static class MockCategory
{
    public static CategoryDto GenerateCategoryDto() => new CategoryDto
    {
        CategoryId = MockDataGenerator.RandomInteger(),
        Id = MockDataGenerator.RandomId(),
        Name = MockDataGenerator.RandomString(10, false),
    };

    public static Category GenerateCategory() => Category.FromDto(GenerateCategoryDto());

    public static List<Category> GenerateCategoryList(int count = -1)
    {
        var categoryCount = count;

        if (categoryCount < 0)
        {
            categoryCount = MockDataGenerator.RandomInteger(1, 5);
        }

        var categories = new List<Category>();

        for (var i = 0; i < categoryCount; i += 1)
        {
            categories.Add(GenerateCategory());
        }

        return categories;
    }
}