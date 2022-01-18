using Microsoft.AspNetCore.Identity;

namespace DigitalFamilyCookbook.Core.Services;

public interface IRoleService
{
    IEnumerable<RoleTypeDto> GetAllRoles();

    Task<string> AddRole(string name);

    Task<string> UpdateRole(string roleTypeId, string name);

    Task<string> DeleteRole(string roleTypeId);
}