namespace DigitalFamilyCookbook.Data.Interfaces;

public interface ICategoryRepository
{
    IEnumerable<Category> GetAllCategories();

    Task<Category> Add(Category category);

    Task<Category> Update(Category category);
}
