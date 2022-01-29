using Microsoft.AspNetCore.Identity;

namespace DigitalFamilyCookbook.Data.Dtos;

public class UserAccountTokenDto : IdentityUserToken<string>
{
    public string UserAccountTokenId { get; set; } = string.Empty;

    public static UserAccountTokenDto None() => new UserAccountTokenDto();
}
