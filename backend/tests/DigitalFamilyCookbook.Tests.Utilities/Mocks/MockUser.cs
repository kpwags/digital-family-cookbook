namespace DigitalFamilyCookbook.Tests.Utilities.Mocks;

public static class MockUser
{
    public static UserAccount GenerateUser()
    {
        var email = MockDataGenerator.RandomEmail();

        return new UserAccount
        {
            Email = email,
            Id = MockDataGenerator.RandomId(),
            Name = $"{MockDataGenerator.RandomString(6, false)} {MockDataGenerator.RandomString(10, false)}",
            UserId = MockDataGenerator.RandomId(),
            UserName = email,
        };
    }
}