using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Commands.System;

public class DeleteRoleType
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
                await _roleService.DeleteRole(command.Id);
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
        public string Id { get; set; } = string.Empty;
    }
}