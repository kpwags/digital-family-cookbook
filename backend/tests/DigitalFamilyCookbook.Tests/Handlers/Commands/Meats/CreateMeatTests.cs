using DigitalFamilyCookbook.Handlers.Commands.Meats;

namespace DigitalFamilyCookbook.Tests.Handlers.Commands.Meats;

public class CreateMeatTests
{
    [Fact]
    public async Task ItSuccessfullyAddsAMeat()
    {
        var meat = MockMeat.GenerateMeat();

        var meatRepository = new Mock<IMeatRepository>();
        meatRepository.Setup(m => m.Add(It.IsAny<Meat>())).ReturnsAsync(meat);

        var handler = new CreateMeat.Handler(meatRepository.Object);

        var result = await handler.Handle(new CreateMeat.Command { Name = meat.Name }, new CancellationToken());

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public async Task ItErrorsIfMeatAlreadyExists()
    {
        var meat = MockMeat.GenerateMeat();

        var meatRepository = new Mock<IMeatRepository>();
        meatRepository.Setup(m => m.Add(It.IsAny<Meat>())).Throws(new Exception($"A meat with the name \"{meat.Name}\" already exists"));

        var handler = new CreateMeat.Handler(meatRepository.Object);

        var result = await handler.Handle(new CreateMeat.Command { Name = meat.Name }, new CancellationToken());

        Assert.False(result.IsSuccessful);
        Assert.Equal($"A meat with the name \"{meat.Name}\" already exists", result.ErrorMessage);
    }
}