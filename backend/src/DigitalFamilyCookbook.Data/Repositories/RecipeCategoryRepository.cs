namespace DigitalFamilyCookbook.Data.Repositories;

public class RecipeCategoryRepository : IRecipeCategoryRepository
{
    private readonly ApplicationDbContext _db;

    public RecipeCategoryRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task AddForRecipe(int recipeId, List<int> categoryIds)
    {
        _db.RecipeCategories.AddRange(categoryIds.Select(i => new RecipeCategoryDto
        {
            Id = Guid.NewGuid().ToString(),
            RecipeId = recipeId,
            CategoryId = i
        }));
        await _db.SaveChangesAsync();
    }

    public async Task DeleteForCategory(int categoryId)
    {
        var recipeCatories = _db.RecipeCategories.Where(rc => rc.CategoryId == categoryId);

        _db.RecipeCategories.RemoveRange(recipeCatories);

        await _db.SaveChangesAsync();
    }

    public async Task DeleteForRecipe(int recipeId)
    {
        var recipeCategories = _db.RecipeCategories.Where(rc => rc.RecipeId == recipeId);

        _db.RecipeCategories.RemoveRange(recipeCategories);

        await _db.SaveChangesAsync();
    }
}