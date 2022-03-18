namespace DigitalFamilyCookbook.IntegrationTests.Mocks;

public static class User
{
    public static UserAccountDto MockAdmin
    {
        get
        {
            return new UserAccountDto
            {
                ConcurrencyStamp = MockDataGenerator.RandomId(),
                Id = "JEANLUCPICARD1701D",
                UserName = "jeanluc.picard@starfleet.gov",
                NormalizedUserName = "JEANLUC.PICARD@STARFLEET.GOV",
                Email = "jeanluc.picard@starfleet.gov",
                NormalizedEmail = "JEANLUC.PICARD@STARFLEET.GOV",
                UserId = "JEANLUCPICARD1701D",
            };
        }
    }

    public static UserAccountDto MockUser
    {
        get
        {
            return new UserAccountDto
            {
                ConcurrencyStamp = MockDataGenerator.RandomId(),
                Id = "BENJAMINSISKO74205",
                UserName = "benjamin.sisko@starfleet.gov",
                NormalizedUserName = "BENJAMIN.SISKO@STARFLEET.GOV",
                Email = "benjamin.sisko@starfleet.gov",
                NormalizedEmail = "BENJAMIN.SISKO@STARFLEET.GOV",
                UserId = "BENJAMINSISKO74205",
            };
        }
    }
}
