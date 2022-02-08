using Microsoft.AspNetCore.Identity;

namespace DigitalFamilyCookbook.Core.Services;

public interface IRoleService
{
    IEnumerable<RoleTypeDto> GetAllRoles();

    Task<RoleType> GetRoleById(string id);

    Task<string> AddRole(string name);

    Task<string> UpdateRole(string id, string name);

    Task<string> DeleteRole(string id);
}