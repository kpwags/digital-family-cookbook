namespace DigitalFamilyCookbook.ApiModels;

public class RoleTypeApiModel
{
    public string Id { get; set; } = string.Empty;

    public string RoleTypeId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string NormalizedName { get; set; } = string.Empty;

    public string ConcurrencyStamp { get; set; } = string.Empty;

    public static RoleTypeApiModel None() => new RoleTypeApiModel();

    public static RoleTypeApiModel FromDomainModel(RoleType role)
    {
        return new RoleTypeApiModel
        {
            RoleTypeId = role.RoleTypeId,
            Name = role.Name,
            NormalizedName = role.NormalizedName,
            ConcurrencyStamp = role.ConcurrencyStamp,
            Id = role.Id,
        };
    }
}
