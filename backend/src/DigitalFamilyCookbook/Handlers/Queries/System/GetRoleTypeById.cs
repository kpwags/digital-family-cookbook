using DigitalFamilyCookbook.Core.Services;
using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Queries.System;

public class GetRoleTypeById
{
    public class Handler : IRequestHandler<Query, RoleTypeApiModel>
    {
        private readonly IRoleService _roleService;

        public Handler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<RoleTypeApiModel> Handle(Query query, CancellationToken cancellationToken)
        {
            var role = await _roleService.GetRoleById(query.Id);

            if (role == null)
            {
                return RoleTypeApiModel.None();
            }

            return RoleTypeApiModel.FromDto(role);
        }
    }

    public class Query : IRequest<RoleTypeApiModel>
    {
        public string Id { get; set; } = string.Empty;
    }
}