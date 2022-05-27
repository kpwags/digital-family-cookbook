namespace DigitalFamilyCookbook.Data.Interfaces;

public interface IRecipeCategoryRepository
{
    Task AddForRecipe(int recipeId, List<int> categoryIds);

    Task DeleteForCategory(int categoryId);

    Task DeleteForRecipe(int recipeId);
}
