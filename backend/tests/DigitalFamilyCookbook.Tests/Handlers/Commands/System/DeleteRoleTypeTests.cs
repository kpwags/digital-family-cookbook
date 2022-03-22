using DigitalFamilyCookbook.Handlers.Commands.System;

namespace DigitalFamilyCookbook.Tests.Handlers.Commands.System;

public class DeleteRoleTypeTests
{
    [Fact]
    public async Task ItSuccessfullyDeletesARole()
    {
        var roleService = new Mock<IRoleService>();
        roleService.Setup(r => r.DeleteRole(It.IsAny<string>()));

        var command = new DeleteRoleType.Command
        {
            Id = Guid.NewGuid().ToString()
        };

        var handler = new DeleteRoleType.Handler(roleService.Object);

        var result = await handler.Handle(command, new CancellationToken());

        Assert.True(result.IsSuccessful);
        Assert.Empty(result.ErrorMessage);
    }

    [Fact]
    public async Task ItErrorsDeletingARoleThatDoesntExist()
    {
        var roleService = new Mock<IRoleService>();
        roleService.Setup(r => r.DeleteRole(It.IsAny<string>())).ThrowsAsync(new Exception("The role to delete was not found"));

        var command = new DeleteRoleType.Command
        {
            Id = Guid.NewGuid().ToString()
        };

        var handler = new DeleteRoleType.Handler(roleService.Object);

        var result = await handler.Handle(command, new CancellationToken());

        Assert.False(result.IsSuccessful);
        Assert.Equal("The role to delete was not found", result.ErrorMessage);
    }
}