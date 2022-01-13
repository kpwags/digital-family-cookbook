using DigitalFamilyCookbook.Core.Services;
using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Commands.System;

public class AddRoleType
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
            var errorMessage = await _roleService.AddRole(command.Name);

            if (errorMessage != string.Empty)
            {
                throw new Exception(errorMessage);
            }

            return Unit.Value;
        }
    }

    public class Command : IRequest<Unit>
    {
        public string Name { get; set; } = string.Empty;
    }
}