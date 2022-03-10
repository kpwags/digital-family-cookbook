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

    public static List<RoleType> GenerateRoleList(int count = -1)
    {
        var roleCount = count;

        if (roleCount < 0)
        {
            roleCount = MockDataGenerator.RandomInteger(1, 3);
        }

        var roles = new List<RoleType>();

        for (var i = 0; i < roleCount; i += 1)
        {
            roles.Add(GenerateRole());
        }

        return roles;
    }
}