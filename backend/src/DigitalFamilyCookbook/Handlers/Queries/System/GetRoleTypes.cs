using DigitalFamilyCookbook.Core.Services;
using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Queries.System;

public class GetRoleTypes
{
    public class Handler : IRequestHandler<Query, IReadOnlyCollection<RoleTypeApiModel>>
    {
        private readonly IRoleService _roleService;

        public Handler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<IReadOnlyCollection<RoleTypeApiModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            var roles = await Task.FromResult(_roleService.GetAllRoles());

            return roles.Select(r => RoleTypeApiModel.FromDomainModel(r)).ToList();
        }
    }

    public class Query : IRequest<IReadOnlyCollection<RoleTypeApiModel>>
    {

    }
}