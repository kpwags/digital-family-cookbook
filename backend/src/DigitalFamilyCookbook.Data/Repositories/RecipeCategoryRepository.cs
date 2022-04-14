namespace DigitalFamilyCookbook.Data.Repositories;

public class RecipeCategoryRepository : IRecipeCategoryRepository
{
    private readonly ApplicationDbContext _db;

    public RecipeCategoryRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task DeleteForCategory(int categoryId)
    {
        var recipeCatories = _db.RecipeCategories.Where(rc => rc.CategoryId == categoryId);

        _db.RecipeCategories.RemoveRange(recipeCatories);

        await _db.SaveChangesAsync();
    }
}