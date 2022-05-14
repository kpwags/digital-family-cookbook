namespace DigitalFamilyCookbook.ApiModels;

public class UserAccountApiModel
{
    public string Id { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public IReadOnlyCollection<RoleTypeApiModel> Roles { get; set; } = Array.Empty<RoleTypeApiModel>();

    public static UserAccountApiModel None() => new UserAccountApiModel();

    public static UserAccountApiModel FromDomainModel(UserAccount model)
    {
        return new UserAccountApiModel
        {
            Id = model.Id,
            UserId = model.UserId,
            Name = model.Name,
            Email = model.Email,
            Roles = model.RoleTypes.Select(r => RoleTypeApiModel.FromDomainModel(r)).ToList(),
        };
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        var model = obj as UserAccountApiModel;

        if (model is null)
        {
            return false;
        }

        return this.Equals(model);
    }

    public bool Equals(UserAccountApiModel model)
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
            && UserId == model.UserId
            && Name == model.Name
            && Email == model.Email;
    }

    public override int GetHashCode() => (Id, UserId, Name, Email).GetHashCode();

    public UserAccount ToDomainModel() => new UserAccount
    {
        Id = this.Id,
        UserId = this.UserId,
        Name = this.Name,
        Email = this.Email,
        RoleTypes = this.Roles.Select(r => r.ToDomainModel()).ToList(),
    };
}