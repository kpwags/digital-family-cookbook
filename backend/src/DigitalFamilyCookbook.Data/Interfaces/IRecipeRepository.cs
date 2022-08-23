namespace DigitalFamilyCookbook.Data.Interfaces;

public interface IRecipeRepository
{
    IEnumerable<Recipe> GetAll();

    Recipe GetById(int recipeId);

    Task<Recipe> Add(Recipe recipe);

    Task Update(Recipe recipe);

    Task Delete(int recipeId);

    Task DeleteRecipeImage(int recipeId);

    IEnumerable<Recipe> GetRecipesForUser(string userAccountId);

    (IEnumerable<Recipe> recipes, int totalRecipes) GetRecipesForUserPaginated(string userAcountId, int currentPage = 1, int recipesPerPage = 10);

    IEnumerable<Recipe> GetRecipesForCategory(int categoryId);

    (IEnumerable<Recipe> recipes, int totalRecipes) GetRecipesForCategoryPaginated(int categoryId, int currentPage = 1, int recipesPerPage = 10);

    IEnumerable<Recipe> GetRecipesForMeat(int meatId);

    (IEnumerable<Recipe> recipes, int totalRecipes) GetRecipesForMeatPaginated(int meatId, int currentPage = 1, int recipesPerPage = 10);
}
