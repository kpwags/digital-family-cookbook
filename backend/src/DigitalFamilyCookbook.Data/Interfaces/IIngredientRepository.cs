namespace DigitalFamilyCookbook.Data.Repositories;

public interface IIngredientRepository
{
    Task<Ingredient> Add(Ingredient ingredient);

    Task Delete(int ingredientId);

    Task DeleteRange(List<int> ingredientIds);

    Task DeleteForRecipe(int recipeId);
}