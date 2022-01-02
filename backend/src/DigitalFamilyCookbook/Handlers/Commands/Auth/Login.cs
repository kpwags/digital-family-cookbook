using DigitalFamilyCookbook.Core.Models;
using DigitalFamilyCookbook.Models;
using DigitalFamilyCookbook.Services;
using MediatR;
using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Commands.Auth;

public class Login
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
            var result = await _authService.VerifyCredentials(cmd.Email, cmd.Password);

            if (result.IsValid)
            {
                var token = _authService.GetAuthToken(result.UserId);

                return new OperationResult<AuthToken>(token);
            }

            return new OperationResult<AuthToken>("Unable to verify username or password");
        }
    }

    public class Command : IRequest<OperationResult<AuthToken>>
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
