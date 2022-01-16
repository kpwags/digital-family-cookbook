using Microsoft.AspNetCore.Identity;

namespace DigitalFamilyCookbook.Data.Dtos;

public class RoleTypeDto : IdentityRole
{
    public string RoleTypeId { get; set; } = string.Empty;

    public UserAccountDto UserAccount { get; set; } = UserAccountDto.None();

    public static RoleTypeDto None() => new RoleTypeDto();
}
