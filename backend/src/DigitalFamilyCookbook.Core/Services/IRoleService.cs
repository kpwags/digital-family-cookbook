using Microsoft.AspNetCore.Identity;

namespace DigitalFamilyCookbook.Core.Services;

public interface IRoleService
{
    IEnumerable<RoleType> GetAllRoles();

    Task<RoleType> GetRoleById(string id);

    Task<string> AddRole(string name);

    Task<string> UpdateRole(string id, string name);

    Task<string> DeleteRole(string id);

    Task<IEnumerable<RoleType>> GetUserRoles(string userId);

    Task AddUserToRole(string userAccountId, string roleName);

    Task DeleteRoleFromUser(string userAccountId, string roleName);
}