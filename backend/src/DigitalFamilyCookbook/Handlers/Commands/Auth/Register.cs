using DigitalFamilyCookbook.Data.Models;
using DigitalFamilyCookbook.Models;
using DigitalFamilyCookbook.Core.Services;
using MediatR;
using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Commands.Auth;

public class Register
{
    public class Handler : IRequestHandler<Command, OperationResult<AuthToken>>
    {
        private readonly IAuthService _authService;

        public Handler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<OperationResult<AuthToken>> Handle(Command cmd, CancellationToken cancellationToken)
        {
            if (cmd.Name.Trim() == string.Empty)
            {
                return new OperationResult<AuthToken>("Name is required");
            }

            if (!Core.Helpers.Validation.IsValidEmailAddress(cmd.Email.Trim()))
            {
                return new OperationResult<AuthToken>("Valid email is required");
            }

            if (cmd.Password != cmd.ConfirmPassword)
            {
                return new OperationResult<AuthToken>("Passwords do not match");
            }

            var result = await _authService.RegisterUser(cmd.Email, cmd.Password, cmd.Name);

            if (result.IsSuccessful)
            {
                return new OperationResult<AuthToken>(result.Token);
            }

            return new OperationResult<AuthToken>("Unable to verify username or password");
        }
    }

    public class Command : IRequest<OperationResult<AuthToken>>
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
