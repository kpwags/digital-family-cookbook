using DigitalFamilyCookbook.Handlers.Queries.Auth;

namespace DigitalFamilyCookbook.Tests.Handlers.Queries.Auth;

public class GetLoggedInUserTests
{
    [Fact]
    public async Task ItSuccessfullyGetsTheUser()
    {
        var userDomainModel = MockUser.GenerateUser();
        var user = ApiModels.UserAccountApiModel.FromDomainModel(userDomainModel);

        var roles = new List<RoleType> {
            new RoleType { Id = Guid.NewGuid().ToString(), RoleTypeId = Guid.NewGuid().ToString(), Name = "Administrator" },
            new RoleType { Id = Guid.NewGuid().ToString(), RoleTypeId = Guid.NewGuid().ToString(), Name = "User" }
        };

        var roleService = new Mock<IRoleService>();
        roleService.Setup(r => r.GetUserRoles(It.IsAny<string>())).ReturnsAsync(roles.AsEnumerable());

        var userAccountRepository = new Mock<IUserAccountRepository>();
        userAccountRepository.Setup(u => u.GetUserAccountByIdOrDefault(It.IsAny<string>())).ReturnsAsync(userDomainModel);

        var httpContextAccessor = MockSession.BuildSession(user);

        var handler = new GetLoggedInUser.Handler(httpContextAccessor.Object, roleService.Object, userAccountRepository.Object);

        var result = await handler.Handle(new GetLoggedInUser.Query(), new CancellationToken());

        Assert.True(user.Equals(result));
        Assert.Equal(2, result.Roles.Count());
    }

    [Fact]
    public async Task ItHandlesNoUserLoggedIn()
    {
        var user = ApiModels.UserAccountApiModel.FromDomainModel(MockUser.GenerateUser());

        var roles = new List<RoleType>();

        var roleService = new Mock<IRoleService>();
        roleService.Setup(r => r.GetUserRoles(It.IsAny<string>())).ReturnsAsync(roles.AsEnumerable());

        var userAccountRepository = new Mock<IUserAccountRepository>();

        var httpContextAccessor = MockSession.BuildSession(null);

        var handler = new GetLoggedInUser.Handler(httpContextAccessor.Object, roleService.Object, userAccountRepository.Object);

        var result = await handler.Handle(new GetLoggedInUser.Query(), new CancellationToken());

        Assert.Equal(string.Empty, result.UserId);
    }

    [Fact]
    public async Task ItHandlesAnInvalidToken()
    {
        var user = ApiModels.UserAccountApiModel.FromDomainModel(MockUser.GenerateUser());

        var roles = new List<RoleType>();

        var roleService = new Mock<IRoleService>();
        roleService.Setup(r => r.GetUserRoles(It.IsAny<string>())).ReturnsAsync(roles.AsEnumerable());

        var userAccountRepository = new Mock<IUserAccountRepository>();
        userAccountRepository.Setup(u => u.GetUserAccountByIdOrDefault(It.IsAny<string>())).ReturnsAsync((UserAccount?)null);

        var httpContextAccessor = MockSession.BuildSession(null);

        var handler = new GetLoggedInUser.Handler(httpContextAccessor.Object, roleService.Object, userAccountRepository.Object);

        var result = await handler.Handle(new GetLoggedInUser.Query(), new CancellationToken());

        Assert.Equal(string.Empty, result.UserId);
    }
}