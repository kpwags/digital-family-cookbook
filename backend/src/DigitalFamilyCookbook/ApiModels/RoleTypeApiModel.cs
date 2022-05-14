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

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        var model = obj as RoleTypeApiModel;

        if (model is null)
        {
            return false;
        }

        return this.Equals(model);
    }

    public bool Equals(RoleTypeApiModel model)
    {
        if (model is null)
        {
            return false;
        }

        if (Object.ReferenceEquals(this, model))
        {
            return true;
        }

        if (this.GetType() != model.GetType())
        {
            return false;
        }

        return Id == model.Id
            && RoleTypeId == model.RoleTypeId
            && Name == model.Name
            && NormalizedName == model.NormalizedName;
    }

    public override int GetHashCode() => (Id, RoleTypeId, Name, NormalizedName).GetHashCode();

    public RoleType ToDomainModel() => new RoleType
    {
        RoleTypeId = this.RoleTypeId,
        Name = this.Name,
        NormalizedName = this.NormalizedName,
        ConcurrencyStamp = this.ConcurrencyStamp,
        Id = this.Id,
    };
}
