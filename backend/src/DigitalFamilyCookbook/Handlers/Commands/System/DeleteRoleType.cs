using DigitalFamilyCookbook.Core.Services;
using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Commands.System;

public class DeleteRoleType
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
            var errorMessage = await _roleService.DeleteRole(command.Id);

            if (errorMessage != string.Empty)
            {
                throw new Exception(errorMessage);
            }

            return Unit.Value;
        }
    }

    public class Command : IRequest<Unit>
    {
        public string Id { get; set; } = string.Empty;
    }
}