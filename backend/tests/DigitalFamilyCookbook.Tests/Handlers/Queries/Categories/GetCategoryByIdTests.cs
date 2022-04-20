using DigitalFamilyCookbook.Handlers.Queries.Categories;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.Categories;

public class GetCategoryByIdTests
{
    [Fact]
    public async Task ItSuccessfullyReturnsAnExistingCategory()
    {
        var category = MockCategory.GenerateCategory();

        var categoryRepository = new Mock<ICategoryRepository>();
        categoryRepository.Setup(c => c.Get(It.IsAny<int>())).Returns(category);

        var handler = new GetCategoryById.Handler(categoryRepository.Object);

        var result = await handler.Handle(new GetCategoryById.Query { Id = category.CategoryId }, new CancellationToken());

        Assert.True(result.IsSuccessful);
        Assert.Equal(category.Name, result.Value?.Name);
    }

    [Fact]
    public async Task ItErrorsIfCategoryDoesNotExist()
    {
        var categoryRepository = new Mock<ICategoryRepository>();
        categoryRepository.Setup(c => c.Get(It.IsAny<int>())).Throws(new Exception("Category not found"));

        var handler = new GetCategoryById.Handler(categoryRepository.Object);

        var result = await handler.Handle(new GetCategoryById.Query { Id = 99 }, new CancellationToken());

        Assert.False(result.IsSuccessful);
        Assert.Equal("Category not found", result.ErrorMessage);
    }
}