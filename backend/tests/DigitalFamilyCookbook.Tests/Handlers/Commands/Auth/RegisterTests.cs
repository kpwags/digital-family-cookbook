using DigitalFamilyCookbook.Handlers.Commands.Auth;

namespace DigitalFamilyCookbook.Tests.Handlers.Commands.Auth;

public class RegisterTests
{
    [Fact]
    public async Task ItSuccessfullyRegistersAUser()
    {
        var siteSettings = MockSiteSettings.GeneratePublicSiteSettings();

        var authService = new Mock<IAuthService>();
        authService.Setup(a => a.RegisterUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new AuthResult
        {
            Error = string.Empty,
            IsSuccessful = true,
            Token = MockDataGenerator.RandomString(150),
        });

        var systemRepository = new Mock<ISystemRepository>();
        systemRepository.Setup(x => x.GetSiteSettings(It.IsAny<int>())).Returns(siteSettings);

        var password = MockDataGenerator.RandomString(10);

        var command = new Register.Command
        {
            Email = MockDataGenerator.RandomEmail(),
            Password = password,
            ConfirmPassword = password,
            Name = MockDataGenerator.RandomString(8),
        };

        var handler = new Register.Handler(authService.Object, systemRepository.Object);

        var authResult = await handler.Handle(command, new CancellationToken());

        authService.Verify(a => a.RegisterUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);

        Assert.True(authResult.IsSuccessful);
        Assert.Empty(authResult.ErrorMessage);
        Assert.NotNull(authResult.Value);
    }

    [Fact]
    public async Task ItErrorsWithDifferentPasswords()
    {
        var siteSettings = MockSiteSettings.GeneratePublicSiteSettings();

        var authService = new Mock<IAuthService>();
        authService.Setup(a => a.RegisterUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new AuthResult
        {
            Error = "Invalid username or password",
            IsSuccessful = false,
            Token = string.Empty,
        });

        var systemRepository = new Mock<ISystemRepository>();
        systemRepository.Setup(x => x.GetSiteSettings(It.IsAny<int>())).Returns(siteSettings);

        var command = new Register.Command
        {
            Email = MockDataGenerator.RandomEmail(),
            Password = MockDataGenerator.RandomString(10),
            ConfirmPassword = MockDataGenerator.RandomString(10),
            Name = MockDataGenerator.RandomString(8),
        };

        var handler = new Register.Handler(authService.Object, systemRepository.Object);

        var authResult = await handler.Handle(command, new CancellationToken());

        authService.Verify(a => a.RegisterUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);

        Assert.False(authResult.IsSuccessful);
        Assert.Equal("Passwords do not match", authResult.ErrorMessage);
        Assert.Null(authResult.Value);
    }

    [Fact]
    public async Task ItErrorsWithInvalidEmail()
    {
        var siteSettings = MockSiteSettings.GeneratePublicSiteSettings();

        var authService = new Mock<IAuthService>();
        authService.Setup(a => a.RegisterUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new AuthResult
        {
            Error = "Valid email is required",
            IsSuccessful = false,
            Token = string.Empty,
        });

        var systemRepository = new Mock<ISystemRepository>();
        systemRepository.Setup(x => x.GetSiteSettings(It.IsAny<int>())).Returns(siteSettings);

        var password = MockDataGenerator.RandomString(10);

        var command = new Register.Command
        {
            Email = MockDataGenerator.RandomString(4),
            Password = password,
            ConfirmPassword = password,
            Name = MockDataGenerator.RandomString(8),
        };

        var handler = new Register.Handler(authService.Object, systemRepository.Object);

        var authResult = await handler.Handle(command, new CancellationToken());

        authService.Verify(a => a.RegisterUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);

        Assert.False(authResult.IsSuccessful);
        Assert.Equal("Valid email is required", authResult.ErrorMessage);
        Assert.Null(authResult.Value);
    }

    [Fact]
    public async Task ItErrorsWithBlankName()
    {
        var siteSettings = MockSiteSettings.GeneratePublicSiteSettings();

        var authService = new Mock<IAuthService>();
        authService.Setup(a => a.RegisterUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new AuthResult
        {
            Error = "Name is required",
            IsSuccessful = false,
            Token = string.Empty,
        });

        var systemRepository = new Mock<ISystemRepository>();
        systemRepository.Setup(x => x.GetSiteSettings(It.IsAny<int>())).Returns(siteSettings);

        var password = MockDataGenerator.RandomString(10);

        var command = new Register.Command
        {
            Email = MockDataGenerator.RandomEmail(),
            Password = password,
            ConfirmPassword = password,
            Name = "",
        };

        var handler = new Register.Handler(authService.Object, systemRepository.Object);

        var authResult = await handler.Handle(command, new CancellationToken());

        authService.Verify(a => a.RegisterUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);

        Assert.False(authResult.IsSuccessful);
        Assert.Equal("Name is required", authResult.ErrorMessage);
        Assert.Null(authResult.Value);
    }

    [Fact]
    public async Task ItValidatesInvitationCodes()
    {
        var siteSettings = MockSiteSettings.GenerateNonPublicSiteSettings();

        var authService = new Mock<IAuthService>();
        authService.Setup(a => a.RegisterUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new AuthResult
        {
            Error = string.Empty,
            IsSuccessful = true,
            Token = MockDataGenerator.RandomString(150),
        });

        var systemRepository = new Mock<ISystemRepository>();
        systemRepository.Setup(x => x.GetSiteSettings(It.IsAny<int>())).Returns(siteSettings);

        var password = MockDataGenerator.RandomString(10);
        var command = new Register.Command
        {
            Email = MockDataGenerator.RandomEmail(),
            Password = password,
            ConfirmPassword = password,
            Name = MockDataGenerator.RandomString(8),
            InvitationCode = siteSettings.InvitationCode,
        };

        var handler = new Register.Handler(authService.Object, systemRepository.Object);

        var authResult = await handler.Handle(command, new CancellationToken());

        authService.Verify(a => a.RegisterUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);

        Assert.True(authResult.IsSuccessful);
        Assert.Empty(authResult.ErrorMessage);
        Assert.NotNull(authResult.Value);
    }

    [Fact]
    public async Task ItErrorsWithoutAValidInvitationCode()
    {
        var siteSettings = MockSiteSettings.GenerateNonPublicSiteSettings();

        var authService = new Mock<IAuthService>();
        authService.Setup(a => a.RegisterUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new AuthResult
        {
            Error = "Invalid username or password",
            IsSuccessful = false,
            Token = string.Empty,
        });

        var systemRepository = new Mock<ISystemRepository>();
        systemRepository.Setup(x => x.GetSiteSettings(It.IsAny<int>())).Returns(siteSettings);

        var password = MockDataGenerator.RandomString(10);
        var command = new Register.Command
        {
            Email = MockDataGenerator.RandomEmail(),
            Password = password,
            ConfirmPassword = password,
            Name = MockDataGenerator.RandomString(8),
            InvitationCode = MockDataGenerator.RandomId(),
        };

        var handler = new Register.Handler(authService.Object, systemRepository.Object);

        var authResult = await handler.Handle(command, new CancellationToken());

        authService.Verify(a => a.RegisterUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);

        Assert.False(authResult.IsSuccessful);
        Assert.Equal("Invalid invitation code", authResult.ErrorMessage);
    }
}