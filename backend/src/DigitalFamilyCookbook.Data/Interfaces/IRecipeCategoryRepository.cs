namespace DigitalFamilyCookbook.Data.Interfaces;

public interface IRecipeCategoryRepository
{
    Task DeleteForCategory(int categoryId);
}
