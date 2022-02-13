namespace DigitalFamilyCookbook.Tests.Utilities.Mocks;

public static class MockRoleType
{
    public static RoleTypeDto GenerateRoleDto()
    {
        var roleName = MockDataGenerator.RandomString(8, false);

        return new RoleTypeDto
        {
            Id = MockDataGenerator.RandomId(),
            Name = roleName,
            RoleTypeId = MockDataGenerator.RandomId(),
            NormalizedName = roleName.ToUpper(),
        };
    }

    public static RoleType GenerateRole()
    {
        return RoleType.FromDto(GenerateRoleDto());
    }
}