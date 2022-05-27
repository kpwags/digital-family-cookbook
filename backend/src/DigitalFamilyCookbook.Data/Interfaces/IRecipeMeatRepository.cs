namespace DigitalFamilyCookbook.Data.Interfaces;

public interface IRecipeMeatRepository
{
    Task AddForRecipe(int recipeId, List<int> categoryIds);

    Task DeleteForMeat(int meatId);

    Task DeleteForRecipe(int recipeId);
}
