using Microsoft.AspNetCore.Identity;

namespace DigitalFamilyCookbook.Data.Dtos;

public class UserAccountDto : IdentityUser
{
    [PersonalData]
    public string UserId { get; set; } = string.Empty;

    [PersonalData]
    public string Name { get; set; } = string.Empty;

    [PersonalData]
    public IEnumerable<RecipeDto> Recipes { get; set; } = Enumerable.Empty<RecipeDto>();

    [PersonalData]
    public IEnumerable<RoleTypeDto> RoleTypes { get; set; } = Enumerable.Empty<RoleTypeDto>();

    [PersonalData]
    public IEnumerable<RefreshTokenDto> RefreshTokens { get; set; } = Enumerable.Empty<RefreshTokenDto>();

    public static UserAccountDto None() => new UserAccountDto();
}
