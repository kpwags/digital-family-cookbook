using DigitalFamilyCookbook.Handlers.Queries.Meats;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.Meats;

public class GetMeatByIdTests
{
    [Fact]
    public async Task ItSuccessfullyReturnsAnExistingMeat()
    {
        var meat = MockMeat.GenerateMeat();

        var meatRepository = new Mock<IMeatRepository>();
        meatRepository.Setup(m => m.Get(It.IsAny<int>())).Returns(meat);

        var handler = new GetMeatById.Handler(meatRepository.Object);

        var result = await handler.Handle(new GetMeatById.Query { Id = meat.MeatId }, new CancellationToken());

        Assert.True(result.IsSuccessful);
        Assert.Equal(meat.Name, result.Value?.Name);
    }

    [Fact]
    public async Task ItErrorsIfMeatDoesNotExist()
    {
        var meatRepository = new Mock<IMeatRepository>();
        meatRepository.Setup(m => m.Get(It.IsAny<int>())).Throws(new Exception("Meat not found"));

        var handler = new GetMeatById.Handler(meatRepository.Object);

        var result = await handler.Handle(new GetMeatById.Query { Id = 99 }, new CancellationToken());

        Assert.False(result.IsSuccessful);
        Assert.Equal("Meat not found", result.ErrorMessage);
    }
}