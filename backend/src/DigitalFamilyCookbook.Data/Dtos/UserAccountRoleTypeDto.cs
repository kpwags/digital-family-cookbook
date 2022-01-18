using Microsoft.AspNetCore.Identity;

namespace DigitalFamilyCookbook.Data.Dtos;

public class UserAccountRoleTypeDto : IdentityUserRole<string>
{
    public string UserAccountRoleTypeId { get; set; } = string.Empty;

    public static UserAccountRoleTypeDto None() => new UserAccountRoleTypeDto();
}
