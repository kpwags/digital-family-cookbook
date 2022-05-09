namespace DigitalFamilyCookbook.ApiModels;

public class IngredientApiModel : BaseApiModel
{
    public string Id { get; set; } = string.Empty;

    public int IngredientId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int? SortOrder { get; set; }

    public static IngredientApiModel None() => new IngredientApiModel();

    public static IngredientApiModel FromDomainModel(Ingredient ingredient)
    {
        return new IngredientApiModel
        {
            Id = ingredient.Id,
            IngredientId = ingredient.IngredientId,
            Name = ingredient.Name,
            SortOrder = ingredient.SortOrder,
            DateCreated = ingredient.DateCreated,
            DateUpdated = ingredient.DateUpdated,
        };
    }
}
