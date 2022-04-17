namespace DigitalFamilyCookbook.Tests.Utilities.Mocks;

public static class MockMeat
{
    public static MeatDto GenerateMeatDto() => new MeatDto
    {
        MeatId = MockDataGenerator.RandomInteger(),
        Id = MockDataGenerator.RandomId(),
        Name = MockDataGenerator.RandomString(10, false),
    };

    public static Meat GenerateMeat() => Meat.FromDto(GenerateMeatDto());

    public static List<Meat> GenerateMeatList(int count = -1)
    {
        var meatCount = count;

        if (meatCount < 0)
        {
            meatCount = MockDataGenerator.RandomInteger(1, 5);
        }

        var meats = new List<Meat>();

        for (var i = 0; i < meatCount; i += 1)
        {
            meats.Add(GenerateMeat());
        }

        return meats;
    }
}