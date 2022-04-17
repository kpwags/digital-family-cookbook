using DigitalFamilyCookbook.Handlers.Commands.Meats;

namespace DigitalFamilyCookbook.Tests.Handlers.Commands.Meats;

public class DeleteMeatTests
{
    [Fact]
    public async Task ItSuccessfullyDeleteAMeat()
    {
        var meat = MockMeat.GenerateMeat();

        var meatRepository = new Mock<IMeatRepository>();
        meatRepository.Setup(m => m.Delete(It.IsAny<int>()));

        var handler = new DeleteMeat.Handler(meatRepository.Object);

        var result = await handler.Handle(new DeleteMeat.Command { Id = meat.MeatId }, new CancellationToken());

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public async Task ItErrorsIfTheCategoryDoesNotExist()
    {
        var meat = MockMeat.GenerateMeat();

        var meatRepository = new Mock<IMeatRepository>();
        meatRepository.Setup(m => m.Delete(It.IsAny<int>())).Throws(new Exception("Meat not found"));

        var handler = new DeleteMeat.Handler(meatRepository.Object);

        var result = await handler.Handle(new DeleteMeat.Command { Id = meat.MeatId }, new CancellationToken());

        Assert.False(result.IsSuccessful);
        Assert.Equal("Meat not found", result.ErrorMessage);
    }
}