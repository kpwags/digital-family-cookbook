using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Queries.System;

public class GetAllUsers
{
    public class Handler : IRequestHandler<Query, OperationResult<IReadOnlyCollection<UserAccountApiModel>>>
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IRoleService _roleService;

        public Handler(IUserAccountRepository userAccountRepository, IRoleService roleService)
        {
            _userAccountRepository = userAccountRepository;
            _roleService = roleService;
        }

        public async Task<OperationResult<IReadOnlyCollection<UserAccountApiModel>>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _userAccountRepository.GetAllUserAccounts();

                if (request.IncludeRoles)
                {
                    var userList = new List<UserAccountApiModel>();

                    foreach (var user in users)
                    {
                        var roles = await _roleService.GetUserRoles(user.Id);

                        user.RoleTypes = roles;

                        userList.Add(UserAccountApiModel.FromDomainModel(user));
                    }

                    return new OperationResult<IReadOnlyCollection<UserAccountApiModel>>(userList.AsReadOnly());
                }
                else
                {
                    return new OperationResult<IReadOnlyCollection<UserAccountApiModel>>(users.Select(u => UserAccountApiModel.FromDomainModel(u)).ToList());
                }
            }
            catch (Exception ex)
            {
                return new OperationResult<IReadOnlyCollection<UserAccountApiModel>>(ex.Message);
            }
        }
    }

    public class Query : IRequest<OperationResult<IReadOnlyCollection<UserAccountApiModel>>>
    {
        public bool IncludeRoles { get; set; }
    }
}