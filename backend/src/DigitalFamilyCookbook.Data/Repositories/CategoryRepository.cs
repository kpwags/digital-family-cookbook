namespace DigitalFamilyCookbook.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _db;

    public CategoryRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public Category Get(int categoryId)
    {
        var c = _db.Categories.FirstOrDefault(c => c.CategoryId == categoryId);

        if (c is null)
        {
            throw new Exception("Category not found");
        }

        return Category.FromDto(c);
    }

    public IEnumerable<Category> GetAllCategories()
    {
        return _db.Categories.Select(c => Category.FromDto(c));
    }

    public async Task<Category> Add(Category category)
    {
        var dto = new CategoryDto
        {
            Id = Guid.NewGuid().ToString(),
            Name = category.Name,
        };

        _db.Categories.Add(dto);

        await _db.SaveChangesAsync();

        return Category.FromDto(dto);
    }

    public async Task<Category> Update(Category category)
    {
        var c = _db.Categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);

        if (c is null)
        {
            throw new Exception("Category not found");
        }

        c.Name = category.Name;

        _db.Categories.Update(c);

        await _db.SaveChangesAsync();

        return Category.FromDto(c);
    }

    public async Task Delete(int categoryId)
    {
        var c = _db.Categories.FirstOrDefault(c => c.CategoryId == categoryId);

        if (c is null)
        {
            throw new Exception("Category not found");
        }

        _db.Categories.Remove(c);

        await _db.SaveChangesAsync();
    }
}