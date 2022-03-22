using DigitalFamilyCookbook.Core.Services;
using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Queries.System;

public class GetRoleTypes
{
    public class Handler : IRequestHandler<Query, OperationResult<IReadOnlyCollection<RoleTypeApiModel>>>
    {
        private readonly IRoleService _roleService;

        public Handler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<OperationResult<IReadOnlyCollection<RoleTypeApiModel>>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var roles = await Task.FromResult(_roleService.GetAllRoles());

                return new OperationResult<IReadOnlyCollection<RoleTypeApiModel>>(roles.Select(r => RoleTypeApiModel.FromDomainModel(r)).ToList());
            }
            catch (Exception ex)
            {
                return new OperationResult<IReadOnlyCollection<RoleTypeApiModel>>(ex.Message);
            }
        }
    }

    public class Query : IRequest<OperationResult<IReadOnlyCollection<RoleTypeApiModel>>>
    {

    }
}