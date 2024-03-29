using DigitalFamilyCookbook.Extensions;
using DigitalFamilyCookbook.Handlers.Commands.Auth;
using Microsoft.AspNetCore.Http;

namespace DigitalFamilyCookbook.Tests.Handlers.Commands.Auth;

public class LoginTests
{
    [Fact]
    public async Task ItSuccessfullyLogsInAUser()
    {
        var authService = new Mock<IAuthService>();
        authService.Setup(a => a.LoginUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new AuthResult
        {
            Error = string.Empty,
            IsSuccessful = true,
            AccessToken = MockDataGenerator.RandomString(150),
        });

        var command = new Login.Command
        {
            Email = MockDataGenerator.RandomEmail(),
            Password = MockDataGenerator.RandomString(10),
        };

        var httpContextAccessor = MockSession.BuildLoginSession();

        var handler = new Login.Handler(authService.Object, httpContextAccessor.Object);

        var authResult = await handler.Handle(command, new CancellationToken());

        authService.Verify(a => a.LoginUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);

        Assert.True(authResult.IsSuccessful);
        Assert.Empty(authResult.ErrorMessage);
        Assert.NotNull(authResult.Value);
    }

    [Fact]
    public async Task ItErrorsWithInvalidUsernameAndPassword()
    {
        var authService = new Mock<IAuthService>();
        authService.Setup(a => a.LoginUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new AuthResult
        {
            Error = "Invalid username or password",
            IsSuccessful = false,
            AccessToken = string.Empty,
        });

        var command = new Login.Command
        {
            Email = MockDataGenerator.RandomEmail(),
            Password = MockDataGenerator.RandomString(10),
        };

        var httpContextAccessor = MockSession.BuildLoginSession();

        var handler = new Login.Handler(authService.Object, httpContextAccessor.Object);

        var authResult = await handler.Handle(command, new CancellationToken());

        authService.Verify(a => a.LoginUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);

        Assert.False(authResult.IsSuccessful);
        Assert.Equal("Unable to verify username or password", authResult.ErrorMessage);
        Assert.Null(authResult.Value);
    }
}