namespace DigitalFamilyCookbook.ApiModels;

public class SiteSettingsApiModel : BaseApiModel
{
    public int SiteSettingsId { get; set; }

    public string Id { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
    
    public string LandingPageText { get; set; } = string.Empty;

    public bool IsPublic { get; set; }

    public bool AllowPublicRegistration { get; set; }

    public string InvitationCode { get; set; } = string.Empty;

    public bool SaveRecipesOnDeleteUser { get; set; }

    public static SiteSettingsApiModel None() => new SiteSettingsApiModel();

    public static SiteSettingsApiModel FromDomainModel(SiteSettings settings)
    {
        return new SiteSettingsApiModel
        {
            SiteSettingsId = settings.SiteSettingsId,
            Id = settings.Id,
            Title = settings.Title,
            LandingPageText = settings .LandingPageText,
            IsPublic = settings.IsPublic,
            AllowPublicRegistration = settings.AllowPublicRegistration,
            InvitationCode = settings.InvitationCode,
            SaveRecipesOnDeleteUser = settings.SaveRecipesOnDeleteUser,
        };
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        var model = obj as SiteSettingsApiModel;

        if (model is null)
        {
            return false;
        }

        return this.Equals(model);
    }

    public bool Equals(SiteSettingsApiModel model)
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
            && SiteSettingsId == model.SiteSettingsId
            && Title == model.Title
            && IsPublic == model.IsPublic
            && AllowPublicRegistration == model.AllowPublicRegistration
            && InvitationCode == model.InvitationCode
            && SaveRecipesOnDeleteUser == model.SaveRecipesOnDeleteUser;
    }

    public override int GetHashCode() => (Id, SiteSettingsId, Title, IsPublic).GetHashCode();
}