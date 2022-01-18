using Microsoft.AspNetCore.Identity;

namespace DigitalFamilyCookbook.Data.Dtos;

public class RoleTypeClaimDto : IdentityRoleClaim<string>
{
    public string RoleClaimId { get; set; } = string.Empty;

    public static RoleTypeClaimDto None() => new RoleTypeClaimDto();
}
