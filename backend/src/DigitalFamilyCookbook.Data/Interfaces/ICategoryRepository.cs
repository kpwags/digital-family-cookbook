namespace DigitalFamilyCookbook.Data.Interfaces;

public interface ICategoryRepository
{
    Category Get(int categoryId);

    IEnumerable<Category> GetAllCategories();

    Task<Category> Add(Category category);

    Task<Category> Update(Category category);

    Task Delete(int categoryId);
}
