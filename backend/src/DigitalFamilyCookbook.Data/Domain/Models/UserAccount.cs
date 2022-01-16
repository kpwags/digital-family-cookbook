using Microsoft.AspNetCore.Identity;

namespace DigitalFamilyCookbook.Data.Domain.Models;

public class UserAccount : IdentityUser
{
    public string UserId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public IEnumerable<Recipe> Recipes { get; set; } = Enumerable.Empty<Recipe>();

    public IEnumerable<RoleType> RoleTypes { get; set; } = Enumerable.Empty<RoleType>();

    public IEnumerable<RefreshToken> RefreshTokens { get; set; } = Enumerable.Empty<RefreshToken>();

    public static UserAccount None() => new UserAccount();

    public static UserAccount FromDto(UserAccountDto dto)
    {
        return new UserAccount
        {
            Id = dto.Id,
            UserId = dto.UserId,
            Name = dto.Name,
            UserName = dto.UserName,
            Email = dto.Email,
            EmailConfirmed = dto.EmailConfirmed,
            Recipes = dto.Recipes.Select(r => Recipe.FromDto(r)),
            RoleTypes = dto.RoleTypes.Select(rt => RoleType.FromDto(rt)),
            RefreshTokens = dto.RefreshTokens.Select(rt => RefreshToken.FromDto(rt)),
        };
    }
}
