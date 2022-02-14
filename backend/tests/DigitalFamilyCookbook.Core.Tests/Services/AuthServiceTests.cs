using DigitalFamilyCookbook.Core.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace DigitalFamilyCookbook.Core.Tests.Services;

public class AuthServiceTests
{
    private DigitalFamilyCookbookConfiguration _configuration;
    private TokenValidationParameters _tokenValidationParameters;
    private Mock<UserManager<UserAccountDto>> _userManager;
    private Mock<SignInManager<UserAccountDto>> _signInManager;
    private Mock<RoleManager<RoleTypeDto>> _roleManager;

    public AuthServiceTests()
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

        _signInManager = new Mock<SignInManager<UserAccountDto>>(
            _userManager.Object,
            new Mock<IHttpContextAccessor>().Object,
            new Mock<IUserClaimsPrincipalFactory<UserAccountDto>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<ILogger<SignInManager<UserAccountDto>>>().Object,
            new Mock<IAuthenticationSchemeProvider>().Object,
            new Mock<IUserConfirmation<UserAccountDto>>().Object);

        _roleManager = new Mock<RoleManager<RoleTypeDto>>(
            new Mock<IRoleStore<RoleTypeDto>>().Object,
            new List<IRoleValidator<RoleTypeDto>>(),
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<ILogger<RoleManager<RoleTypeDto>>>().Object
        );

        _configuration = new DigitalFamilyCookbookConfiguration
        {
            Auth = new AuthConfiguration
            {
                JwtLifespan = 30,
                JwtSecret = Guid.NewGuid().ToString(),
            },
        };

        _tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(_configuration.Auth.JwtSecret)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            RequireExpirationTime = false,
            ClockSkew = TimeSpan.Zero,
        };
    }

    [Fact]
    public async Task ItSuccessfullyRegistersAUser()
    {
        var createdUser = MockUser.GenerateUserDto();

#nullable disable
        _userManager
            .SetupSequence(u => u.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((UserAccountDto)null)
            .ReturnsAsync(createdUser);
#nullable enable

        _userManager.Setup(u => u.CreateAsync(It.IsAny<UserAccountDto>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
        _userManager.Setup(u => u.GetClaimsAsync(It.IsAny<UserAccountDto>())).ReturnsAsync(new List<Claim>());
        _userManager.Setup(u => u.GetRolesAsync(It.IsAny<UserAccountDto>())).ReturnsAsync(new List<string>());

        var authService = new AuthService(
            _configuration,
            _userManager.Object,
            _signInManager.Object,
            _roleManager.Object,
            _tokenValidationParameters
        );

        var result = await authService.RegisterUser(createdUser.Email, MockDataGenerator.RandomString(12), MockDataGenerator.RandomString(10));

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public async Task ItWontRegisterUserIfEmailExists()
    {
        _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new UserAccountDto { Email = MockDataGenerator.RandomEmail() });
        _userManager.Setup(u => u.CreateAsync(It.IsAny<UserAccountDto>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
        _userManager.Setup(u => u.GetClaimsAsync(It.IsAny<UserAccountDto>())).ReturnsAsync(new List<Claim>());
        _userManager.Setup(u => u.GetRolesAsync(It.IsAny<UserAccountDto>())).ReturnsAsync(new List<string>());

        var authService = new AuthService(
            _configuration,
            _userManager.Object,
            _signInManager.Object,
            _roleManager.Object,
            _tokenValidationParameters
        );

        var email = MockDataGenerator.RandomEmail();

        var result = await authService.RegisterUser(email, MockDataGenerator.RandomString(12), MockDataGenerator.RandomString(10));

        Assert.False(result.IsSuccessful);
        Assert.Equal($"{email} already exists", result.Error);
    }

    [Fact]
    public async Task ItSuccessfullyLogsUserIn()
    {
        var user = MockUser.GenerateUserDto();

        _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
        _userManager.Setup(u => u.GetClaimsAsync(It.IsAny<UserAccountDto>())).ReturnsAsync(new List<Claim>());
        _userManager.Setup(u => u.GetRolesAsync(It.IsAny<UserAccountDto>())).ReturnsAsync(new List<string>());

        _signInManager.Setup(s => s.CheckPasswordSignInAsync(It.IsAny<UserAccountDto>(), It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(SignInResult.Success);

        var authService = new AuthService(
            _configuration,
            _userManager.Object,
            _signInManager.Object,
            _roleManager.Object,
            _tokenValidationParameters
        );

        var result = await authService.LoginUser(user.Email, MockDataGenerator.RandomString(12));

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public async Task ItFailsWhenItCantFindAUser()
    {
        var user = MockUser.GenerateUserDto();

#nullable disable
        _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((UserAccountDto)null);
#nullable enable
        _userManager.Setup(u => u.GetClaimsAsync(It.IsAny<UserAccountDto>())).ReturnsAsync(new List<Claim>());
        _userManager.Setup(u => u.GetRolesAsync(It.IsAny<UserAccountDto>())).ReturnsAsync(new List<string>());

        _signInManager.Setup(s => s.CheckPasswordSignInAsync(It.IsAny<UserAccountDto>(), It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(SignInResult.Success);

        var authService = new AuthService(
            _configuration,
            _userManager.Object,
            _signInManager.Object,
            _roleManager.Object,
            _tokenValidationParameters
        );

        var result = await authService.LoginUser(user.Email, MockDataGenerator.RandomString(12));

        Assert.False(result.IsSuccessful);
        Assert.Equal("Invalid email or password", result.Error);
    }

    [Fact]
    public async Task ItFailsWhenPasswordCheckFails()
    {
        var user = MockUser.GenerateUserDto();

        _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
        _userManager.Setup(u => u.GetClaimsAsync(It.IsAny<UserAccountDto>())).ReturnsAsync(new List<Claim>());
        _userManager.Setup(u => u.GetRolesAsync(It.IsAny<UserAccountDto>())).ReturnsAsync(new List<string>());

        _signInManager.Setup(s => s.CheckPasswordSignInAsync(It.IsAny<UserAccountDto>(), It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(SignInResult.Failed);

        var authService = new AuthService(
            _configuration,
            _userManager.Object,
            _signInManager.Object,
            _roleManager.Object,
            _tokenValidationParameters
        );

        var result = await authService.LoginUser(user.Email, MockDataGenerator.RandomString(12));

        Assert.False(result.IsSuccessful);
        Assert.Equal("Invalid email or password", result.Error);
    }
}