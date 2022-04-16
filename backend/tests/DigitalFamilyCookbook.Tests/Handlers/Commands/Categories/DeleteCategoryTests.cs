using DigitalFamilyCookbook.Handlers.Commands.Categories;

namespace DigitalFamilyCookbook.Tests.Handlers.Commands.Categories;

public class DeleteCategtoryTests
{
    [Fact]
    public async Task ItSuccessfullyDeleteACategory()
    {
        var category = MockCategory.GenerateCategory();

        var categoryRepository = new Mock<ICategoryRepository>();
        categoryRepository.Setup(c => c.Delete(It.IsAny<int>()));

        var handler = new DeleteCategory.Handler(categoryRepository.Object);

        var result = await handler.Handle(new DeleteCategory.Command { Id = category.CategoryId }, new CancellationToken());

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public async Task ItErrorsIfTheCategoryDoesNotExist()
    {
        var category = MockCategory.GenerateCategory();

        var categoryRepository = new Mock<ICategoryRepository>();
        categoryRepository.Setup(c => c.Delete(It.IsAny<int>())).Throws(new Exception("Category not found"));

        var handler = new DeleteCategory.Handler(categoryRepository.Object);

        var result = await handler.Handle(new DeleteCategory.Command { Id = category.CategoryId }, new CancellationToken());

        Assert.False(result.IsSuccessful);
        Assert.Equal("Category not found", result.ErrorMessage);
    }
}