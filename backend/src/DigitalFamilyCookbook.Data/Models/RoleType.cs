namespace DigitalFamilyCookbook.Data.Models;

public class RoleType
{
    public string Id { get; set; } = string.Empty;

    public int RoleTypeId { get; set; }

    public string Name { get; set; } = string.Empty;

    public IEnumerable<UserAccountRoleType> UserAccountRoleTypes { get; set; } = Enumerable.Empty<UserAccountRoleType>();

    public UserAccount UserAccount { get; set; } = UserAccount.None();

    public static RoleType None() => new RoleType();
}
