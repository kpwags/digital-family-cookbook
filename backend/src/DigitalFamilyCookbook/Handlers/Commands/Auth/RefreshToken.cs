namespace DigitalFamilyCookbook.Handlers.Commands.Auth;

public class RefreshToken
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
            var result = await _authService.RefreshToken(cmd.Token, cmd.IpAddress);

            if (result.IsSuccessful)
            {
                return new OperationResult<AuthResult>(result);
            }

            return new OperationResult<AuthResult>("Unable to refresh token");
        }
    }

    public class Command : IRequest<OperationResult<AuthResult>>
    {
        public string Token { get; set; } = string.Empty;

        public string IpAddress { get; set; } = string.Empty;
    }
}
