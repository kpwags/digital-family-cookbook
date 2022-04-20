using DigitalFamilyCookbook.Handlers.Queries.Meats;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.Meats;

public class GetAllMeatsTests
{
    [Fact]
    public async Task ItSuccessfullyReturnsAllMeats()
    {
        var meats = MockMeat.GenerateMeatList(6);

        var meatRepository = new Mock<IMeatRepository>();
        meatRepository.Setup(m => m.GetAll()).Returns(meats);

        var handler = new GetAllMeats.Handler(meatRepository.Object);

        var result = await handler.Handle(new GetAllMeats.Query(), new CancellationToken());

        Assert.Equal(6, result.Value?.Count);
    }
}