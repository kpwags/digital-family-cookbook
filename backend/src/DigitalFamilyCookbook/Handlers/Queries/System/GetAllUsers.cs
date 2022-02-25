using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Queries.System;

public class GetAllUsers
{
    public class Handler : IRequestHandler<Query, IReadOnlyCollection<UserAccountApiModel>>
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IRoleService _roleService;

        public Handler(IUserAccountRepository userAccountRepository, IRoleService roleService)
        {
            _userAccountRepository = userAccountRepository;
            _roleService = roleService;
        }

        public async Task<IReadOnlyCollection<UserAccountApiModel>> Handle(Query request, CancellationToken cancellationToken)
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

                return userList.AsReadOnly();
            }
            else
            {
                return users.Select(u => UserAccountApiModel.FromDomainModel(u)).ToList();
            }
        }
    }

    public class Query : IRequest<IReadOnlyCollection<UserAccountApiModel>>
    {
        public bool IncludeRoles { get; set; }
    }
}