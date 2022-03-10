using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace DigitalFamilyCookbook.Core.Tests.Services;

public class RoleServiceTests
{
    private Mock<UserManager<UserAccountDto>> _userManager;
    private Mock<RoleManager<RoleTypeDto>> _roleManager;
    private Mock<ILogger<RoleService>> _logger;

    public RoleServiceTests()
    {
        _userManager = new Mock<UserManager<UserAccountDto>>(
            new Mock<IUserStore<UserAccountDto>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<UserAccountDto>>().Object,
            new IUserValidator<UserAccountDto>[0],
            new IPasswordValidator<UserAccountDto>[0],
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<UserAccountDto>>>().Object);

        _roleManager = new Mock<RoleManager<RoleTypeDto>>(
            new Mock<IRoleStore<RoleTypeDto>>().Object,
            new List<IRoleValidator<RoleTypeDto>>(),
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<ILogger<RoleManager<RoleTypeDto>>>().Object
        );

        _logger = new Mock<ILogger<RoleService>>();
    }

    #region GetAllRoles

    [Fact]
    public void ItRetrievesAllRoles()
    {
        _roleManager.Setup(r => r.Roles).Returns(new List<RoleTypeDto> {
            new RoleTypeDto { Id = Guid.NewGuid().ToString(), RoleTypeId = Guid.NewGuid().ToString(), Name = "Administrator" },
            new RoleTypeDto { Id = Guid.NewGuid().ToString(), RoleTypeId = Guid.NewGuid().ToString(), Name = "User" }
        }.AsQueryable());

        var logger = new Mock<ILogger<RoleService>>().Object;

        var roleService = new RoleService(
            _roleManager.Object,
            _userManager.Object,
            _logger.Object
        );

        var roles = roleService.GetAllRoles();

        Assert.Equal(2, roles.Count());
    }

    #endregion

    #region AddRole

    [Fact]
    public async Task ItSuccessfullyAddsARole()
    {
        _roleManager.Setup(r => r.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
        _roleManager.Setup(r => r.CreateAsync(It.IsAny<RoleTypeDto>())).ReturnsAsync(IdentityResult.Success);

        var roleService = new RoleService(
            _roleManager.Object,
            _userManager.Object,
            _logger.Object
        );

        var result = await roleService.AddRole("User");

        Assert.Empty(result);
    }

    [Fact]
    public async Task ItDoesntAddARoleIfExists()
    {
        _roleManager.Setup(r => r.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
        _roleManager.Setup(r => r.CreateAsync(It.IsAny<RoleTypeDto>())).ReturnsAsync(IdentityResult.Success);

        var roleService = new RoleService(
            _roleManager.Object,
            _userManager.Object,
            _logger.Object
        );

        var result = await roleService.AddRole("User");

        Assert.Equal("Role already exists", result);
    }

    #endregion

    #region UpdateRole

    [Fact]
    public async Task ItSuccessfullyUpdatesARole()
    {
        var roleTypeId = Guid.NewGuid().ToString();

        _roleManager.Setup(r => r.Roles).Returns(new List<RoleTypeDto> {
            new RoleTypeDto { Id = Guid.NewGuid().ToString(), RoleTypeId = Guid.NewGuid().ToString(), Name = "Administrator" },
            new RoleTypeDto { Id = roleTypeId, RoleTypeId = Guid.NewGuid().ToString(), Name = "User" }
        }.AsQueryable());

        _roleManager.Setup(r => r.UpdateAsync(It.IsAny<RoleTypeDto>())).ReturnsAsync(IdentityResult.Success);

        var roleService = new RoleService(
            _roleManager.Object,
            _userManager.Object,
            _logger.Object
        );

        var result = await roleService.UpdateRole(roleTypeId, "User");

        Assert.Empty(result);
    }

    [Fact]
    public async Task ItAlertsTheUserARoleCannotBeFoundToUpdate()
    {
        _roleManager.Setup(r => r.Roles).Returns(new List<RoleTypeDto> {
            new RoleTypeDto { Id = Guid.NewGuid().ToString(), RoleTypeId = Guid.NewGuid().ToString(), Name = "Administrator" },
            new RoleTypeDto { Id = Guid.NewGuid().ToString(), RoleTypeId = Guid.NewGuid().ToString(), Name = "User" }
        }.AsQueryable());

        _roleManager.Setup(r => r.UpdateAsync(It.IsAny<RoleTypeDto>())).ReturnsAsync(IdentityResult.Success);

        var roleService = new RoleService(
            _roleManager.Object,
            _userManager.Object,
            _logger.Object
        );

        var result = await roleService.UpdateRole(Guid.NewGuid().ToString(), "User");

        Assert.Equal("The role to update was not found", result);
    }

    #endregion

    #region DeleteRole

    [Fact]
    public async Task ItSuccessfullyDeletesARole()
    {
        var roleTypeId = Guid.NewGuid().ToString();

        _roleManager.Setup(r => r.Roles).Returns(new List<RoleTypeDto> {
            new RoleTypeDto { Id = Guid.NewGuid().ToString(), RoleTypeId = Guid.NewGuid().ToString(), Name = "Administrator" },
            new RoleTypeDto { Id = roleTypeId, RoleTypeId = Guid.NewGuid().ToString(), Name = "User" }
        }.AsQueryable());

        _roleManager.Setup(r => r.DeleteAsync(It.IsAny<RoleTypeDto>())).ReturnsAsync(IdentityResult.Success);

        var roleService = new RoleService(
            _roleManager.Object,
            _userManager.Object,
            _logger.Object
        );

        var result = await roleService.DeleteRole(roleTypeId);

        Assert.Empty(result);
    }

    [Fact]
    public async Task ItAlertsTheUserARoleCannotBeFoundToDelete()
    {
        _roleManager.Setup(r => r.Roles).Returns(new List<RoleTypeDto> {
            new RoleTypeDto { Id = Guid.NewGuid().ToString(), RoleTypeId = Guid.NewGuid().ToString(), Name = "Administrator" },
            new RoleTypeDto { Id = Guid.NewGuid().ToString(), RoleTypeId = Guid.NewGuid().ToString(), Name = "User" }
        }.AsQueryable());

        _roleManager.Setup(r => r.UpdateAsync(It.IsAny<RoleTypeDto>())).ReturnsAsync(IdentityResult.Success);

        var roleService = new RoleService(
            _roleManager.Object,
            _userManager.Object,
            _logger.Object
        );

        var result = await roleService.DeleteRole(Guid.NewGuid().ToString());

        Assert.Equal("The role to delete was not found", result);
    }

    #endregion

    #region GetUserRoles

    [Fact]
    public async Task ItRetrievesAUsersRole()
    {
        _userManager.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new UserAccountDto { Email = MockDataGenerator.RandomEmail() });
        _userManager.Setup(u => u.GetRolesAsync(It.IsAny<UserAccountDto>())).ReturnsAsync(new List<string> { "Administrator", "User" });

        _roleManager.Setup(r => r.FindByNameAsync("User")).ReturnsAsync(new RoleTypeDto { Id = Guid.NewGuid().ToString(), RoleTypeId = Guid.NewGuid().ToString(), Name = "User" });
        _roleManager.Setup(r => r.FindByNameAsync("Administrator")).ReturnsAsync(new RoleTypeDto { Id = Guid.NewGuid().ToString(), RoleTypeId = Guid.NewGuid().ToString(), Name = "Administrator" });

        var logger = new Mock<ILogger<RoleService>>().Object;

        var roleService = new RoleService(
            _roleManager.Object,
            _userManager.Object,
            _logger.Object
        );

        var roles = await roleService.GetUserRoles("123");

        Assert.Equal(2, roles.Count());
    }

    #endregion

    #region AddUserToRole

    [Fact]
    public async Task ItAddsAUserToARole()
    {
        var user = MockUser.GenerateUser();
        var role = MockRoleType.GenerateRoleDto();

        var roles = new List<RoleTypeDto>
        {
            role,
        };

        _userManager.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new UserAccountDto { Email = MockDataGenerator.RandomEmail() });
        _userManager.Setup(u => u.AddToRoleAsync(It.IsAny<UserAccountDto>(), It.IsAny<string>()));

        _roleManager.Setup(r => r.Roles).Returns(roles.AsQueryable());

        var logger = new Mock<ILogger<RoleService>>().Object;

        var roleService = new RoleService(
            _roleManager.Object,
            _userManager.Object,
            _logger.Object
        );

        await roleService.AddUserToRole(user.Id, role.Name);

        _userManager.Verify(u => u.FindByIdAsync(It.IsAny<string>()), Times.Once);
        _userManager.Verify(u => u.AddToRoleAsync(It.IsAny<UserAccountDto>(), It.IsAny<string>()), Times.Once);
    }

    #endregion

    #region DeleteRoleFromUser

    [Fact]
    public async Task ItRemovessAUserFromARole()
    {
        var user = MockUser.GenerateUser();
        var role = MockRoleType.GenerateRoleDto();

        var roles = new List<RoleTypeDto>
        {
            role,
        };

        _userManager.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new UserAccountDto { Email = MockDataGenerator.RandomEmail() });
        _userManager.Setup(u => u.RemoveFromRoleAsync(It.IsAny<UserAccountDto>(), It.IsAny<string>()));

        _roleManager.Setup(r => r.Roles).Returns(roles.AsQueryable());

        var logger = new Mock<ILogger<RoleService>>().Object;

        var roleService = new RoleService(
            _roleManager.Object,
            _userManager.Object,
            _logger.Object
        );

        await roleService.DeleteRoleFromUser(user.Id, role.Name);

        _userManager.Verify(u => u.FindByIdAsync(It.IsAny<string>()), Times.Once);
        _userManager.Verify(u => u.RemoveFromRoleAsync(It.IsAny<UserAccountDto>(), It.IsAny<string>()), Times.Once);
    }

    #endregion
}