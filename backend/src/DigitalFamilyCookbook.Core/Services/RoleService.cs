using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DigitalFamilyCookbook.Core.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<RoleTypeDto> _roleManager;
    private readonly UserManager<UserAccountDto> _userManager;
    private readonly ILogger<RoleService> _logger;

    public RoleService(RoleManager<RoleTypeDto> roleManager, UserManager<UserAccountDto> userManager, ILogger<RoleService> logger)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _logger = logger;
    }

    public IEnumerable<RoleType> GetAllRoles()
    {
        return _roleManager.Roles.Select(r => RoleType.FromDto(r)).OrderBy(r => r.Name).AsEnumerable();
    }

    public async Task<RoleType> GetRoleById(string id)
    {
        var roleType = await _roleManager.FindByIdAsync(id);

        if (roleType is null)
        {
            roleType = RoleTypeDto.None();
        }

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

        var result = await _roleManager.CreateAsync(new RoleTypeDto
        {
            Name = name,
            RoleTypeId = Guid.NewGuid().ToString()
        });

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

    public async Task<string> UpdateRole(string id, string name)
    {
        var role = _roleManager.Roles.FirstOrDefault(r => r.Id == id);

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

    public async Task<string> DeleteRole(string id)
    {
        var role = _roleManager.Roles.FirstOrDefault(r => r.Id == id);

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

    public async Task<IEnumerable<RoleType>> GetUserRoles(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        var roles = await _userManager.GetRolesAsync(user);

        var rolesList = new List<RoleType>();

        foreach (var roleName in roles)
        {
            var role = _roleManager.FindByNameAsync(roleName);

            if (role != null)
            {
                rolesList.Add(RoleType.FromDto(role.Result));
            }
        }

        return rolesList.AsEnumerable();
    }
}