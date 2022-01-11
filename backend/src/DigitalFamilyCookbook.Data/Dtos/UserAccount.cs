using Microsoft.AspNetCore.Identity;

namespace DigitalFamilyCookbook.Data.Dtos;

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

    [PersonalData]
    public IEnumerable<RefreshToken> RefreshTokens { get; set; } = Enumerable.Empty<RefreshToken>();

    public static UserAccount None() => new UserAccount();
}
