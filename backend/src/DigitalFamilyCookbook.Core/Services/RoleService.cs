using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DigitalFamilyCookbook.Core.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<RoleTypeDto> _roleManager;
    private readonly ILogger<RoleService> _logger;

    public RoleService(RoleManager<RoleTypeDto> roleManager, ILogger<RoleService> logger)
    {
        _roleManager = roleManager;
        _logger = logger;
    }

    public IEnumerable<RoleTypeDto> GetAllRoles()
    {
        return _roleManager.Roles.OrderBy(r => r.Name).AsEnumerable();
    }

    public async Task<RoleType> GetRoleById(string id)
    {
        var roleType = await _roleManager.FindByIdAsync(id);

        return RoleType.FromDto(roleType);
    }

    public async Task<string> AddRole(string name)
    {
        var doesRoleExist = await _roleManager.RoleExistsAsync(name);

        if (doesRoleExist)
        {
            _logger.LogInformation($"The role '{name}' already exists");
            return "Role already exists";
        }

        var result = await _roleManager.CreateAsync(new RoleTypeDto { Name = name });

        if (!result.Succeeded)
        {
            _logger.LogInformation($"There was an error adding the role {name}");

            var error = result.Errors.FirstOrDefault();

            if (error == null)
            {
                return "Error adding role";
            }

            return error.Description;
        }

        _logger.LogInformation($"The role, {name} has been added successfully");
        return string.Empty;
    }

    public async Task<string> UpdateRole(string roleTypeId, string name)
    {
        var role = _roleManager.Roles.FirstOrDefault(r => r.Id == roleTypeId);

        if (role == null)
        {
            _logger.LogInformation("The role to update was not found");
            return "The role to update was not found";
        }

        role.Name = name;

        var result = await _roleManager.UpdateAsync(role);

        if (!result.Succeeded)
        {
            _logger.LogInformation($"There was an error updating the role {name}");

            var error = result.Errors.FirstOrDefault();

            if (error == null)
            {
                return "Error updating role";
            }

            return error.Description;
        }

        _logger.LogInformation($"The role, {name} has been updating successfully");
        return string.Empty;
    }

    public async Task<string> DeleteRole(string roleTypeId)
    {
        var role = _roleManager.Roles.FirstOrDefault(r => r.Id == roleTypeId);

        if (role == null)
        {
            _logger.LogInformation("The role to delete was not found");
            return "The role to delete was not found";
        }

        var result = await _roleManager.DeleteAsync(role);

        if (!result.Succeeded)
        {
            _logger.LogInformation("There was an error deleting the role");

            var error = result.Errors.FirstOrDefault();

            if (error == null)
            {
                return "Error deleting role";
            }

            return error.Description;
        }

        _logger.LogInformation($"The role has been deleted successfully");
        return string.Empty;
    }
}