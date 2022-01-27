namespace DigitalFamilyCookbook.Tests.Utilities.Helpers;

public static class MockDataGenerator
{
    public static string RandomId()
    {
        return Guid.NewGuid().ToString();
    }

    public static string RandomString(int length, bool allowSpaces = true)
    {
        Random random = new Random(Guid.NewGuid().GetHashCode());
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz" + (allowSpaces ? " " : "");

        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string RandomString(int minLength, int maxLength)
    {
        return RandomString(RandomInteger(minLength, maxLength));
    }

    public static int RandomInteger(int min = 1, int max = 100)
    {
        Random random = new Random(Guid.NewGuid().GetHashCode());

        return random.Next(min, max);
    }

    public static bool RandomBoolean()
    {
        Random random = new Random(Guid.NewGuid().GetHashCode());

        return random.Next(0, 100) % 2 == 0;
    }

    public static string RandomEmail()
    {
        return $"{RandomString(10, false)}@{RandomString(6)}.com";
    }

    public static char RandomGender()
    {
        Random random = new Random(Guid.NewGuid().GetHashCode());

        return (random.Next(0, 100) % 2 == 0) ? 'M' : 'F';
    }

    public static DateTime RandomDate()
    {
        return new DateTime(
            MockDataGenerator.RandomInteger(1970, 2000),
            MockDataGenerator.RandomInteger(1, 12),
            MockDataGenerator.RandomInteger(1, 28),
            MockDataGenerator.RandomInteger(0, 23),
            MockDataGenerator.RandomInteger(0, 59),
            MockDataGenerator.RandomInteger(0, 59)
        );
    }

    public static byte[] RandomByteArray(int sizeInKb)
    {
        Random rnd = new Random();
        byte[] b = new byte[sizeInKb * 1024];
        rnd.NextBytes(b);
        return b;
    }
}
