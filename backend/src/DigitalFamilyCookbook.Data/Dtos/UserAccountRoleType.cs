namespace DigitalFamilyCookbook.Data.Dtos;

public class UserAccountRoleType
{
    public string Id { get; set; } = string.Empty;

    public int UserAccountRoleTypeId { get; set; }

    public string UserAccountId { get; set; } = string.Empty;

    public UserAccount UserAccount { get; set; } = UserAccount.None();

    public int RoleTypeId { get; set; }

    public RoleType RoleType { get; set; } = RoleType.None();
}
