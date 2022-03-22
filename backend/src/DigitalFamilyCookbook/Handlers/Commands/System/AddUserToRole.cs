using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Commands.System;

public class AddUserToRole
{
    public class Handler : IRequestHandler<Command, OperationResult<string>>
    {
        private readonly IRoleService _roleService;

        public Handler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<OperationResult<string>> Handle(Command command, CancellationToken cancellationToken)
        {
            try
            {
                await _roleService.AddUserToRole(command.UserAccountId, command.RoleName);
            }
            catch (Exception ex)
            {
                return new OperationResult<string>(false, "", ex.Message);
            }

            return new OperationResult<string>(true, "");
        }
    }

    public class Command : IRequest<OperationResult<string>>
    {
        public string UserAccountId { get; set; } = string.Empty;

        public string RoleName { get; set; } = string.Empty;
    }
}