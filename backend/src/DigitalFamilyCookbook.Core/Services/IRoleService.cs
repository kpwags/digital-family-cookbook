namespace DigitalFamilyCookbook.Core.Services;

public interface IRoleService
{
    IEnumerable<RoleType> GetAllRoles();

    Task<string> AddRole(string name);

    Task<string> UpdateRole(string roleTypeId, string name);

    Task<string> DeleteRole(string roleTypeId);
}