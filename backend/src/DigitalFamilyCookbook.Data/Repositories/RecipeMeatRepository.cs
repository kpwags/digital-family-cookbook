namespace DigitalFamilyCookbook.Data.Repositories;

public class RecipeMeatRepository : IRecipeMeatRepository
{
    private readonly ApplicationDbContext _db;

    public RecipeMeatRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task DeleteForMeat(int meatId)
    {
        var recipeMeats = _db.RecipeMeats.Where(rm => rm.MeatId == meatId);

        _db.RecipeMeats.RemoveRange(recipeMeats);

        await _db.SaveChangesAsync();
    }
}