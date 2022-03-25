using Microsoft.AspNetCore.Http;
using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Queries.Auth;

public class GetLoggedInUser
{
    public class Handler : IRequestHandler<Query, UserAccountApiModel>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRoleService _roleService;
        private readonly IUserAccountRepository _userAccountRepository;

        public Handler(IHttpContextAccessor httpContextAccessor, IRoleService roleService, IUserAccountRepository userAccountRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _roleService = roleService;
            _userAccountRepository = userAccountRepository;
        }

        public async Task<UserAccountApiModel> Handle(Query query, CancellationToken cancellationToken)
        {
            if (_httpContextAccessor.HttpContext is not null)
            {
                var currentUser = _httpContextAccessor.HttpContext.CurrentUser(false);

                if (currentUser.Id != string.Empty)
                {
                    var userAccount = await _userAccountRepository.GetUserAccountByIdOrDefault(currentUser.Id);

                    if (userAccount is null)
                    {
                        return UserAccountApiModel.None();
                    }

                    var roles = await _roleService.GetUserRoles(currentUser.Id);

                    currentUser.Roles = roles.Select(r => RoleTypeApiModel.FromDomainModel(r)).ToList();

                    return currentUser;
                }
            }

            return UserAccountApiModel.None();
        }
    }

    public class Query : IRequest<UserAccountApiModel>
    {

    }
}