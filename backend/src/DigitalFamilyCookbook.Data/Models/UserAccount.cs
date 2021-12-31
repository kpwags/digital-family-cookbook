using Microsoft.AspNetCore.Identity;

namespace DigitalFamilyCookbook.Data.Models;

public class UserAccount : IdentityUser
{
    [PersonalData]
    public string UserId { get; set; } = string.Empty;

    [PersonalData]
    public string Name { get; set; } = string.Empty;

    [PersonalData]
    public IEnumerable<Recipe> Recipes { get; set; } = Enumerable.Empty<Recipe>();

    [PersonalData]
    public IEnumerable<RoleType> RoleTypes { get; set; } = Enumerable.Empty<RoleType>();

    [PersonalData]
    public IEnumerable<UserAccountRoleType> UserAccountRoleTypes { get; set; } = Enumerable.Empty<UserAccountRoleType>();

    public static UserAccount None() => new UserAccount();
}
