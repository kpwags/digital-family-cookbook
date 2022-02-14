using DigitalFamilyCookbook.Handlers.Queries.System;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.System;

public class GetRoleTypeByIdTests
{
    [Fact]
    public async Task ItSuccessfullyReturnsARole()
    {
        var role = MockRoleType.GenerateRole();
        var roleApiModel = ApiModels.RoleTypeApiModel.FromDomainModel(role);

        var roleService = new Mock<IRoleService>();
        roleService.Setup(r => r.GetRoleById(It.IsAny<string>())).ReturnsAsync(role);

        var command = new GetRoleTypeById.Query
        {
            Id = role.Id,
        };

        var handler = new GetRoleTypeById.Handler(roleService.Object);

        var result = await handler.Handle(command, new CancellationToken());

        Assert.True(roleApiModel.Equals(result));
    }

    [Fact]
    public async Task ItHandlesANotFoundRoleReturned()
    {
        var roleService = new Mock<IRoleService>();
        roleService.Setup(r => r.GetRoleById(It.IsAny<string>())).ReturnsAsync(RoleType.None());

        var command = new GetRoleTypeById.Query
        {
            Id = Guid.NewGuid().ToString(),
        };

        var handler = new GetRoleTypeById.Handler(roleService.Object);

        var result = await handler.Handle(command, new CancellationToken());

        Assert.Equal(string.Empty, result.RoleTypeId);
        Assert.Null(result.Name);
    }
}