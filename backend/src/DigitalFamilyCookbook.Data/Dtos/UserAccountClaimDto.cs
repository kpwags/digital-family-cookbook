using Microsoft.AspNetCore.Identity;

namespace DigitalFamilyCookbook.Data.Dtos;

public class UserAccountClaimDto : IdentityUserClaim<string>
{
    public string UserAccountClaimId { get; set; } = string.Empty;

    public static UserAccountClaimDto None() => new UserAccountClaimDto();
}
