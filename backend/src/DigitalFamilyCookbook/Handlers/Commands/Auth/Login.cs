using DigitalFamilyCookbook.Data.Models;
using DigitalFamilyCookbook.Models;
using DigitalFamilyCookbook.Core.Services;
using MediatR;
using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Commands.Auth;

public class Login
{
    public class Handler : IRequestHandler<Command, OperationResult<AuthResult>>
    {
        private readonly IAuthService _authService;

        public Handler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<OperationResult<AuthResult>> Handle(Command cmd, CancellationToken cancellationToken)
        {
            var result = await _authService.LoginUser(cmd.Email, cmd.Password);

            if (result.IsSuccessful)
            {
                return new OperationResult<AuthResult>(result);
            }

            return new OperationResult<AuthResult>("Unable to verify username or password");
        }
    }

    public class Command : IRequest<OperationResult<AuthResult>>
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
