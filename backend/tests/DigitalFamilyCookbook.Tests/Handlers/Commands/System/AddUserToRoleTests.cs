using DigitalFamilyCookbook.Handlers.Commands.System;

namespace DigitalFamilyCookbook.Tests.Handlers.Commands.System;

public class AddUserToRoleTests
{
    [Fact]
    public async Task ItSuccessfullyAddsARoleToAUser()
    {
        var roleService = new Mock<IRoleService>();
        roleService.Setup(r => r.AddUserToRole(It.IsAny<string>(), It.IsAny<string>()));

        var command = new AddUserToRole.Command
        {
            RoleName = MockDataGenerator.RandomString(8, false),
            UserAccountId = MockDataGenerator.RandomId(),
        };

        var handler = new AddUserToRole.Handler(roleService.Object);

        var result = await handler.Handle(command, new CancellationToken());

        Assert.True(result.IsSuccessful);
        Assert.Empty(result.ErrorMessage);
    }
}