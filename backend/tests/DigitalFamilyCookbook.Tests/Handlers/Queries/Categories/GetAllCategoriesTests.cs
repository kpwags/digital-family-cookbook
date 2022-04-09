using DigitalFamilyCookbook.Handlers.Queries.Categories;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.System;

public class GetAllCategoriesTests
{
    [Fact]
    public async Task ItSuccessfullyReturnsAllCategories()
    {
        var categories = MockCategory.GenerateCategoryList(6);

        var categoryRepository = new Mock<ICategoryRepository>();
        categoryRepository.Setup(c => c.GetAllCategories()).Returns(categories);

        var handler = new GetAllCategories.Handler(categoryRepository.Object);

        var result = await handler.Handle(new GetAllCategories.Query(), new CancellationToken());

        Assert.Equal(6, result.Value?.Count);
    }
}