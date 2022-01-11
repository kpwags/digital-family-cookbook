using Microsoft.AspNetCore.Identity;

namespace DigitalFamilyCookbook.Data.Dtos;

public class RoleType : IdentityRole
{
    public string RoleTypeId { get; set; } = string.Empty;

    public UserAccount UserAccount { get; set; } = UserAccount.None();

    public static RoleType None() => new RoleType();
}
