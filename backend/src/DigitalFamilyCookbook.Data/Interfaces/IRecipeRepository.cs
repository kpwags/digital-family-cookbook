namespace DigitalFamilyCookbook.Data.Interfaces;

public interface IRecipeRepository
{
    IEnumerable<Recipe> GetAll(bool includePrivate = true);

    Recipe GetById(int recipeId);

    Task<Recipe> Add(Recipe recipe);

    Task Update(Recipe recipe);

    Task Delete(int recipeId);

    Task DeleteRecipeImage(int recipeId);

    IEnumerable<Recipe> GetRecipesForUser(string userAccountId, bool includePrivate);

    (IEnumerable<Recipe> recipes, int totalRecipes) GetRecipesForUserPaginated(string userAcountId, bool includePrivate, int currentPage = 1, int recipesPerPage = 10);

    IEnumerable<Recipe> GetRecipesForCategory(int categoryId, bool isLoggedIn);

    (IEnumerable<Recipe> recipes, int totalRecipes) GetRecipesForCategoryPaginated(int categoryId, bool includePrivate, int currentPage = 1, int recipesPerPage = 10);

    IEnumerable<Recipe> GetRecipesForMeat(int meatId, bool includePrivate);

    (IEnumerable<Recipe> recipes, int totalRecipes) GetRecipesForMeatPaginated(int meatId, bool includePrivate, int currentPage = 1, int recipesPerPage = 10);

    (IEnumerable<Recipe> recipes, int totalRecipes) GetAllRecipesPaginated(bool includePrivate, int currentPage = 1, int recipesPerPage = 10);

    Task MarkRecipeAsFavorite(string userAccountId, int recipeId);

    Task RemoveRecipeAsFavorite(string userAccountId, int recipeId);

    bool IsRecipeFavoriteForUser(string userAccountId, int recipeId);

    (IEnumerable<Recipe> recipes, int totalRecipes) GetFavoriteRecipesForUserPaginated(string userAccountId, int currentPage = 1, int recipesPerPage = 10);

    IEnumerable<Recipe> GetRecentRecipes(int count, bool includePrivate);
    
    IEnumerable<Recipe> GetMostFavoritedRecipes(int count, bool includePrivate);
    
    (IEnumerable<Recipe> recipes, int totalRecipes) SearchRecipesPaginated(string keywords, bool includePrivate, int currentPage = 1, int recipesPerPage = 10);
    
    IEnumerable<Recipe> QuickSearchRecipes(string keywords, bool includePrivate, int count = 10);
}
