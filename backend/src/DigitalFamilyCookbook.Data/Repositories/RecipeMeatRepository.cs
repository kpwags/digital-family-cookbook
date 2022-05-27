namespace DigitalFamilyCookbook.Data.Repositories;

public class RecipeMeatRepository : IRecipeMeatRepository
{
    private readonly ApplicationDbContext _db;

    public RecipeMeatRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task AddForRecipe(int recipeId, List<int> meatIds)
    {
        _db.RecipeMeats.AddRange(meatIds.Select(i => new RecipeMeatDto
        {
            Id = Guid.NewGuid().ToString(),
            RecipeId = recipeId,
            MeatId = i,
        }));
        await _db.SaveChangesAsync();
    }

    public async Task DeleteForMeat(int meatId)
    {
        var recipeMeats = _db.RecipeMeats.Where(rm => rm.MeatId == meatId);

        _db.RecipeMeats.RemoveRange(recipeMeats);

        await _db.SaveChangesAsync();
    }

    public async Task DeleteForRecipe(int recipeId)
    {
        var recipeMeats = _db.RecipeMeats.Where(rm => rm.RecipeId == recipeId);

        _db.RecipeMeats.RemoveRange(recipeMeats);

        await _db.SaveChangesAsync();
    }
}