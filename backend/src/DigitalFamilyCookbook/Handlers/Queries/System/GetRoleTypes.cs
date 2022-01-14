using DigitalFamilyCookbook.Core.Services;
using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Queries.System;

public class GetRoleTypes
{
    public class Handler : IRequestHandler<Query, IReadOnlyCollection<RoleType>>
    {
        private readonly IRoleService _roleService;

        public Handler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<IReadOnlyCollection<RoleType>> Handle(Query request, CancellationToken cancellationToken)
        {
            var roles = await Task.FromResult(_roleService.GetAllRoles());

            return roles.ToList();
        }
    }

    public class Query : IRequest<IReadOnlyCollection<RoleType>>
    {

    }
}