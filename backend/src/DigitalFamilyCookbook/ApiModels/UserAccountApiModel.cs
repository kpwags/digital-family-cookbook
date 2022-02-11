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
}