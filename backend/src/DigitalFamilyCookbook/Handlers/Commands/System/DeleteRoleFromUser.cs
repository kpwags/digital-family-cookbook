using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Commands.System;

public class DeleteRoleFromUser
{
    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IRoleService _roleService;

        public Handler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
        {
            await _roleService.DeleteRoleFromUser(command.UserAccountId, command.RoleName);

            return Unit.Value;
        }
    }

    public class Command : IRequest<Unit>
    {
        public string UserAccountId { get; set; } = string.Empty;

        public string RoleName { get; set; } = string.Empty;
    }
}