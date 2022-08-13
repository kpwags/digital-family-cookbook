namespace DigitalFamilyCookbook.ApiModels;

public class RecipeListPageResults : BaseApiModel
{
    public string PageTitle { get; set; } = string.Empty;

    public IReadOnlyCollection<RecipeApiModel> Recipes { get; set; } = Array.Empty<RecipeApiModel>();
}