using DigitalFamilyCookbook.Handlers.Queries.System;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.System;

public class GetRoleTypesTests
{
    [Fact]
    public async Task ItSuccessfullyReturnsRoles()
    {
        var roles = new List<RoleType> {
            MockRoleType.GenerateRole(),
            MockRoleType.GenerateRole()
        };

        var roleService = new Mock<IRoleService>();
        roleService.Setup(r => r.GetAllRoles()).Returns(roles.AsEnumerable());

        var handler = new GetRoleTypes.Handler(roleService.Object);

        var result = await handler.Handle(new GetRoleTypes.Query(), new CancellationToken());

        Assert.Equal(2, result.Count());
    }
}