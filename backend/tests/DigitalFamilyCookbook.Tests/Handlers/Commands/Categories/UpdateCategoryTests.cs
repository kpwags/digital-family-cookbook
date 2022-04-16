using DigitalFamilyCookbook.Handlers.Commands.Categories;

namespace DigitalFamilyCookbook.Tests.Handlers.Commands.Categories;

public class UpdateCategtoryTests
{
    [Fact]
    public async Task ItSuccessfullyUpdatesACategory()
    {
        var category = MockCategory.GenerateCategory();

        var categoryRepository = new Mock<ICategoryRepository>();
        categoryRepository.Setup(c => c.Update(It.IsAny<Category>())).ReturnsAsync(category);

        var handler = new UpdateCategory.Handler(categoryRepository.Object);

        var result = await handler.Handle(new UpdateCategory.Command { Id = category.CategoryId, Name = category.Name }, new CancellationToken());

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public async Task ItErrorsIfCategoryAlreadyExists()
    {
        var category = MockCategory.GenerateCategory();

        var categoryRepository = new Mock<ICategoryRepository>();
        categoryRepository.Setup(c => c.Update(It.IsAny<Category>())).Throws(new Exception($"A category with the name \"{category.Name}\" already exists"));

        var handler = new UpdateCategory.Handler(categoryRepository.Object);

        var result = await handler.Handle(new UpdateCategory.Command { Id = category.CategoryId, Name = category.Name }, new CancellationToken());

        Assert.False(result.IsSuccessful);
        Assert.Equal($"A category with the name \"{category.Name}\" already exists", result.ErrorMessage);
    }
}