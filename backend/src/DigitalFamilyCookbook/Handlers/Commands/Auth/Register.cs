using DigitalFamilyCookbook.Extensions;

namespace DigitalFamilyCookbook.Handlers.Commands.Auth;

public class Register
{
    public class Handler : IRequestHandler<Command, OperationResult<AuthResult>>
    {
        private readonly IAuthService _authService;
        private readonly ISystemRepository _systemRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Handler(IAuthService authService, ISystemRepository systemRepository, IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            _systemRepository = systemRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<OperationResult<AuthResult>> Handle(Command cmd, CancellationToken cancellationToken)
        {
            if (cmd.Name.Trim() == string.Empty)
            {
                return new OperationResult<AuthResult>("Name is required");
            }

            if (!Core.Helpers.Validation.IsValidEmailAddress(cmd.Email.Trim()))
            {
                return new OperationResult<AuthResult>("Valid email is required");
            }

            if (cmd.Password != cmd.ConfirmPassword)
            {
                return new OperationResult<AuthResult>("Passwords do not match");
            }

            var siteSettings = _systemRepository.GetSiteSettings(1);
            if (!siteSettings.AllowPublicRegistration && cmd.InvitationCode != siteSettings.InvitationCode)
            {
                return new OperationResult<AuthResult>("Invalid invitation code");
            }

            var ip = "";
            if (_httpContextAccessor.HttpContext is not null)
            {
                ip = _httpContextAccessor.HttpContext.GetUserIpAddress();
            }

            var result = await _authService.RegisterUser(cmd.Email, cmd.Password, cmd.Name, ip);

            if (result.IsSuccessful)
            {
                return new OperationResult<AuthResult>(result);
            }

            return new OperationResult<AuthResult>(result.Error);
        }
    }

    public class Command : IRequest<OperationResult<AuthResult>>
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ConfirmPassword { get; set; } = string.Empty;

        public string InvitationCode { get; set; } = string.Empty;
    }
}
