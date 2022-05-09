using Microsoft.AspNetCore.Identity;

namespace DigitalFamilyCookbook.Data.Dtos;

public class UserAccountDto : IdentityUser
{
    [PersonalData]
    public string UserId { get; set; } = string.Empty;

    [PersonalData]
    public string Name { get; set; } = string.Empty;

    [PersonalData]
    public List<RecipeDto> Recipes { get; set; } = new List<RecipeDto>();

    public static UserAccountDto None() => new UserAccountDto();
}
