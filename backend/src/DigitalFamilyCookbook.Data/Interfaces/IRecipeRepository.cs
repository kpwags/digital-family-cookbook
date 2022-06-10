namespace DigitalFamilyCookbook.Data.Interfaces;

public interface IRecipeRepository
{
    IEnumerable<Recipe> GetAll();

    Recipe GetById(int recipeId);

    Task<Recipe> Add(Recipe recipe);

    Task Update(Recipe recipe);

    Task Delete(int recipeId);

    Task DeleteRecipeImage(int recipeId);

    IEnumerable<Recipe> GetUserRecipes(string userAccountId);
}
