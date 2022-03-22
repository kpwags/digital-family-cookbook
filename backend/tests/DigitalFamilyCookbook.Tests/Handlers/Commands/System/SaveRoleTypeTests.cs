using DigitalFamilyCookbook.Handlers.Commands.System;

namespace DigitalFamilyCookbook.Tests.Handlers.Commands.System;

public class SaveRoleTypeTests
{
    [Fact]
    public async Task ItSuccessfullyAddsARole()
    {
        var roleService = new Mock<IRoleService>();
        roleService.Setup(r => r.AddRole(It.IsAny<string>()));

        var command = new SaveRoleType.Command
        {
            Id = string.Empty,
            Name = MockDataGenerator.RandomString(10),
        };

        var handler = new SaveRoleType.Handler(roleService.Object);

        var result = await handler.Handle(command, new CancellationToken());

        Assert.True(result.IsSuccessful);
        Assert.Empty(result.ErrorMessage);
    }

    [Fact]
    public async Task ItErrorsAddingARoleThatAlreadyEsists()
    {
        var roleService = new Mock<IRoleService>();
        roleService.Setup(r => r.AddRole(It.IsAny<string>())).ThrowsAsync(new Exception("Role already exists"));

        var command = new SaveRoleType.Command
        {
            Id = string.Empty,
            Name = MockDataGenerator.RandomString(10),
        };

        var handler = new SaveRoleType.Handler(roleService.Object);

        var result = await handler.Handle(command, new CancellationToken());

        Assert.False(result.IsSuccessful);
        Assert.Equal("Role already exists", result.ErrorMessage);
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

        Assert.True(result.IsSuccessful);
        Assert.Empty(result.ErrorMessage);
    }

    [Fact]
    public async Task ItErrorsUpdatingARoleThatDoesntExist()
    {
        var roleService = new Mock<IRoleService>();
        roleService.Setup(r => r.UpdateRole(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception("The role to update was not found"));

        var command = new SaveRoleType.Command
        {
            Id = Guid.NewGuid().ToString(),
            Name = MockDataGenerator.RandomString(10),
        };

        var handler = new SaveRoleType.Handler(roleService.Object);

        var result = await handler.Handle(command, new CancellationToken());

        Assert.False(result.IsSuccessful);
        Assert.Equal("The role to update was not found", result.ErrorMessage);
    }
}