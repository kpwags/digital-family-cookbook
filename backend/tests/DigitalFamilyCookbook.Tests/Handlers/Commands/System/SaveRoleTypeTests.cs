
using DigitalFamilyCookbook.Handlers.Commands.System;

namespace DigitalFamilyCookbook.Tests.Handlers.Commands.System;

public class SaveRoleTypeTests
{
    [Fact]
    public async Task ItSuccessfullyAddsARole()
    {
        var roleService = new Mock<IRoleService>();
        roleService.Setup(r => r.AddRole(It.IsAny<string>())).ReturnsAsync("");

        var command = new SaveRoleType.Command
        {
            Id = string.Empty,
            Name = MockDataGenerator.RandomString(10),
        };

        var handler = new SaveRoleType.Handler(roleService.Object);

        var result = await handler.Handle(command, new CancellationToken());

        Assert.Equal(Unit.Value, result);
    }

    [Fact]
    public async Task ItErrorsAddingARoleThatAlreadyEsists()
    {
        var roleService = new Mock<IRoleService>();
        roleService.Setup(r => r.AddRole(It.IsAny<string>())).ReturnsAsync("Role already exists");

        var command = new SaveRoleType.Command
        {
            Id = string.Empty,
            Name = MockDataGenerator.RandomString(10),
        };

        var handler = new SaveRoleType.Handler(roleService.Object);

        var ex = await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(command, new CancellationToken()));

        Assert.Equal("Role already exists", ex.Message);
    }

    [Fact]
    public async Task ItSuccessfullyUpdatesARole()
    {
        var roleService = new Mock<IRoleService>();
        roleService.Setup(r => r.UpdateRole(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("");

        var command = new SaveRoleType.Command
        {
            Id = Guid.NewGuid().ToString(),
            Name = MockDataGenerator.RandomString(10),
        };

        var handler = new SaveRoleType.Handler(roleService.Object);

        var result = await handler.Handle(command, new CancellationToken());

        Assert.Equal(Unit.Value, result);
    }

    [Fact]
    public async Task ItErrorsUpdatingARoleThatDoesntExist()
    {
        var roleService = new Mock<IRoleService>();
        roleService.Setup(r => r.UpdateRole(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("The role to update was not found");

        var command = new SaveRoleType.Command
        {
            Id = Guid.NewGuid().ToString(),
            Name = MockDataGenerator.RandomString(10),
        };

        var handler = new SaveRoleType.Handler(roleService.Object);

        var ex = await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(command, new CancellationToken()));

        Assert.Equal("The role to update was not found", ex.Message);
    }
}