using DigitalFamilyCookbook.Handlers.Queries.System;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.System;

public class GetAllUsersTests
{
    [Fact]
    public async Task ItSuccessfullyReturnsAllUsers()
    {
        var users = new List<UserAccount> {
            MockUser.GenerateUser(),
            MockUser.GenerateUser(),
            MockUser.GenerateUser(),
        };

        var userRepository = new Mock<IUserAccountRepository>();
        userRepository.Setup(u => u.GetAllUserAccounts()).ReturnsAsync(users);

        var roleService = new Mock<IRoleService>();
        roleService.Setup(r => r.GetAllRoles()).Returns(MockRoleType.GenerateRoleList());

        var handler = new GetAllUsers.Handler(userRepository.Object, roleService.Object);

        var result = await handler.Handle(new GetAllUsers.Query(), new CancellationToken());

        Assert.Equal(3, result.Value?.Count);
    }
}