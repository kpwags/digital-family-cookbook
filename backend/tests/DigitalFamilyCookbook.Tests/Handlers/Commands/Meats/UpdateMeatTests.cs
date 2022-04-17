using DigitalFamilyCookbook.Handlers.Commands.Meats;

namespace DigitalFamilyCookbook.Tests.Handlers.Commands.Meats;

public class UpdateMeatTests
{
    [Fact]
    public async Task ItSuccessfullyUpdatesAMeat()
    {
        var meat = MockMeat.GenerateMeat();

        var meatRepository = new Mock<IMeatRepository>();
        meatRepository.Setup(m => m.Update(It.IsAny<Meat>())).ReturnsAsync(meat);

        var handler = new UpdateMeat.Handler(meatRepository.Object);

        var result = await handler.Handle(new UpdateMeat.Command { Id = meat.MeatId, Name = meat.Name }, new CancellationToken());

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public async Task ItErrorsIfCategoryAlreadyExists()
    {
        var meat = MockMeat.GenerateMeat();

        var meatRepository = new Mock<IMeatRepository>();
        meatRepository.Setup(c => c.Update(It.IsAny<Meat>())).Throws(new Exception($"A meat with the name \"{meat.Name}\" already exists"));

        var handler = new UpdateMeat.Handler(meatRepository.Object);

        var result = await handler.Handle(new UpdateMeat.Command { Id = meat.MeatId, Name = meat.Name }, new CancellationToken());

        Assert.False(result.IsSuccessful);
        Assert.Equal($"A meat with the name \"{meat.Name}\" already exists", result.ErrorMessage);
    }
}