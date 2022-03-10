using DigitalFamilyCookbook.Handlers.Commands.System;

namespace DigitalFamilyCookbook.Tests.Handlers.Commands.System;

public class DeleteRoleFromUserTests
{
    [Fact]
    public async Task ItSuccessfullyDeletesARoleFromUser()
    {
        var roleService = new Mock<IRoleService>();
        roleService.Setup(r => r.DeleteRoleFromUser(It.IsAny<string>(), It.IsAny<string>()));

        var command = new DeleteRoleFromUser.Command
        {
            RoleName = MockDataGenerator.RandomString(8, false),
            UserAccountId = MockDataGenerator.RandomId(),
        };

        var handler = new DeleteRoleFromUser.Handler(roleService.Object);

        var result = await handler.Handle(command, new CancellationToken());

        Assert.Equal(Unit.Value, result);
    }
}