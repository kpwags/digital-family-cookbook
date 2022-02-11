using DigitalFamilyCookbook.Core.Services;
using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Queries.Auth;

public class GetUserRoles
{
    public class Handler : IRequestHandler<Query, IReadOnlyCollection<RoleTypeApiModel>>
    {
        private readonly IRoleService _roleService;

        public Handler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<IReadOnlyCollection<RoleTypeApiModel>> Handle(Query query, CancellationToken cancellationToken)
        {
            var roles = await _roleService.GetUserRoles(query.Id);

            return roles.Select(r => RoleTypeApiModel.FromDomainModel(r)).ToList();
        }
    }

    public class Query : IRequest<IReadOnlyCollection<RoleTypeApiModel>>
    {
        public string Id { get; set; } = string.Empty;
    }
}