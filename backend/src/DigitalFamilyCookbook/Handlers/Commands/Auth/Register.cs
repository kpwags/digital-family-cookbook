using DigitalFamilyCookbook.Core.Services;
using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Commands.Auth;

public class Register
{
    public class Handler : IRequestHandler<Command, OperationResult<AuthResult>>
    {
        private readonly IAuthService _authService;
        private readonly ISystemRepository _systemRepository;

        public Handler(IAuthService authService, ISystemRepository systemRepository)
        {
            _authService = authService;
            _systemRepository = systemRepository;
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

            var result = await _authService.RegisterUser(cmd.Email, cmd.Password, cmd.Name);

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
