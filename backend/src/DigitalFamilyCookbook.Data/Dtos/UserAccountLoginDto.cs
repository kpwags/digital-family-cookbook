using Microsoft.AspNetCore.Identity;

namespace DigitalFamilyCookbook.Data.Dtos;

public class UserAccountLoginDto : IdentityUserLogin<string>
{
    public string UserAccountLoginId { get; set; } = string.Empty;

    public static UserAccountLoginDto None() => new UserAccountLoginDto();
}
