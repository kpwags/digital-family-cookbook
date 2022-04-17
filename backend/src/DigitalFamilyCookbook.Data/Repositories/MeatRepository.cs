namespace DigitalFamilyCookbook.Data.Repositories;

public class MeatRepository : IMeatRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IRecipeMeatRepository _recipeMeatRepository;

    public MeatRepository(ApplicationDbContext db, IRecipeMeatRepository recipeMeatRepository)
    {
        _db = db;
        _recipeMeatRepository = recipeMeatRepository;
    }

    public Meat Get(int meatId)
    {
        var m = _db.Meats.FirstOrDefault(m => m.MeatId == meatId);

        if (m is null)
        {
            throw new Exception("Category not found");
        }

        return Meat.FromDto(m);
    }

    public IEnumerable<Meat> GetAll()
    {
        return _db.Meats.Select(m => Meat.FromDto(m));
    }

    public async Task<Meat> Add(Meat meat)
    {
        if (_db.Meats.Any(m => m.Name.ToLower() == meat.Name.ToLower()))
        {
            throw new Exception($"A meat with the name \"{meat.Name}\" already exists");
        }

        var dto = new MeatDto
        {
            Id = Guid.NewGuid().ToString(),
            Name = meat.Name,
        };

        _db.Meats.Add(dto);

        await _db.SaveChangesAsync();

        return Meat.FromDto(dto);
    }

    public async Task<Meat> Update(Meat meat)
    {
        if (_db.Meats.Any(m => m.Name.ToLower() == meat.Name.ToLower() && m.MeatId != meat.MeatId))
        {
            throw new Exception($"A meat with the name \"{meat.Name}\" already exists");
        }

        var m = _db.Meats.FirstOrDefault(m => m.MeatId == meat.MeatId);

        if (m is null)
        {
            throw new Exception("Meat not found");
        }

        m.Name = meat.Name;

        _db.Meats.Update(m);

        await _db.SaveChangesAsync();

        return Meat.FromDto(m);
    }

    public async Task Delete(int meatId)
    {
        var m = _db.Meats.FirstOrDefault(m => m.MeatId == meatId);

        if (m is null)
        {
            throw new Exception("Category not found");
        }

        await _recipeMeatRepository.DeleteForMeat(meatId);

        _db.Meats.Remove(m);

        await _db.SaveChangesAsync();
    }
}