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

    public static UserAccountDto None() => new UserAccountDto();
}
