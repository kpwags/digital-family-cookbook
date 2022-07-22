namespace DigitalFamilyCookbook.Handlers.Commands.Auth;

public class RevokeToken
{
    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Handler(IAuthService authService, IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(Command cmd, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(cmd.Token))
            {
                throw new Exception("Invalid token");
            }

            var ip = "";
            if (_httpContextAccessor.HttpContext is not null)
            {
                ip = _httpContextAccessor.HttpContext.GetUserIpAddress();
            }

            await _authService.RevokeToken(cmd.Token, ip);

            return Unit.Value;
        }
    }

    public class Command : IRequest<Unit>
    {
        public string Token { get; set; } = string.Empty;
    }
}
