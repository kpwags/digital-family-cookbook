namespace DigitalFamilyCookbook.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IRecipeCategoryRepository _recipeCategoryRepository;

    public CategoryRepository(ApplicationDbContext db, IRecipeCategoryRepository recipeCategoryRepository)
    {
        _db = db;
        _recipeCategoryRepository = recipeCategoryRepository;
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
        if (_db.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower()))
        {
            throw new Exception($"A category with the name \"{category.Name}\" already exists");
        }

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
        if (_db.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower() && c.CategoryId != category.CategoryId))
        {
            throw new Exception($"A category with the name \"{category.Name}\" already exists");
        }

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

        await _recipeCategoryRepository.DeleteForCategory(categoryId);

        _db.Categories.Remove(c);

        await _db.SaveChangesAsync();
    }

    public IEnumerable<Category> GetForRecipe(int recipeId)
    {
        return _db.RecipeCategories
            .Where(rc => rc.RecipeId == recipeId)
            .Select(rc => Category.FromDto(rc.Category));
    }
}