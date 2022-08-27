using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalFamilyCookbook.Data.Dtos;

public class UserAccountDto : IdentityUser
{
    [PersonalData]
    public string UserId { get; set; } = string.Empty;

    [PersonalData]
    public string Name { get; set; } = string.Empty;

    [PersonalData]
    public List<RecipeDto> Recipes { get; set; } = new List<RecipeDto>();

    [PersonalData]
    public List<RefreshTokenDto> RefreshTokens { get; set; } = new List<RefreshTokenDto>();

    [NotMapped]
    public IEnumerable<RoleTypeDto> RoleTypes { get; set; } = Enumerable.Empty<RoleTypeDto>();

    [PersonalData]
    public List<RecipeFavoriteDto> RecipeFavorites { get; set; } = new List<RecipeFavoriteDto>();

    public static UserAccountDto None() => new UserAccountDto();
}
