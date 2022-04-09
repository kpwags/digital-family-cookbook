using DigitalFamilyCookbook.Handlers.Commands.Categories;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.System;

public class CreateCategtoryTests
{
    [Fact]
    public async Task ItSuccessfullyReturnsAnExistingCategory()
    {
        var category = MockCategory.GenerateCategory();

        var categoryRepository = new Mock<ICategoryRepository>();
        categoryRepository.Setup(c => c.Add(It.IsAny<Category>())).ReturnsAsync(category);

        var handler = new CreateCategory.Handler(categoryRepository.Object);

        var result = await handler.Handle(new CreateCategory.Command { Name = category.Name }, new CancellationToken());

        Assert.True(result.IsSuccessful);
    }

    // [Fact]
    // public async Task ItErrorsIfCategoryDoesNotExist()
    // {
    //     var categoryRepository = new Mock<ICategoryRepository>();
    //     categoryRepository.Setup(c => c.Get(It.IsAny<int>())).Throws(new Exception("Category not found"));

    //     var handler = new GetCategoryById.Handler(categoryRepository.Object);

    //     var result = await handler.Handle(new GetCategoryById.Query(), new CancellationToken());

    //     Assert.False(result.IsSuccessful);
    //     Assert.Equal(result.ErrorMessage, "Category not found");
    // }
}