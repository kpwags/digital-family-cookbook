using DigitalFamilyCookbook.Handlers.Commands.System;

namespace DigitalFamilyCookbook.Tests.Handlers.Commands.System;

public class DeleteUserAccountTests
{
    [Fact]
    public async Task ItSuccessfullyDeletesAUserAccount()
    {
        var userRepository = new Mock<IUserAccountRepository>();
        userRepository.Setup(u => u.DeleteUserAccount(It.IsAny<string>()));

        var command = new DeleteUserAccount.Command
        {
            Id = MockDataGenerator.RandomId(),
        };

        var handler = new DeleteUserAccount.Handler(userRepository.Object);

        var result = await handler.Handle(command, new CancellationToken());

        Assert.True(result.IsSuccessful);
        Assert.Empty(result.ErrorMessage);
    }
}