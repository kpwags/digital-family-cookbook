using DigitalFamilyCookbook.Core.Services;
using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Commands.System;

public class SaveRoleType
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
            string errorMessage = string.Empty;

            if (command.Id == string.Empty)
            {
                errorMessage = await _roleService.AddRole(command.Name);
            }
            else
            {
                errorMessage = await _roleService.UpdateRole(command.Id, command.Name);
            }

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

        public string Name { get; set; } = string.Empty;
    }
}