namespace DigitalFamilyCookbook.Data.Interfaces;

public interface IMeatRepository
{
    Meat Get(int meatId);

    IEnumerable<Meat> GetAll();

    Task<Meat> Add(Meat meat);

    Task<Meat> Update(Meat meat);

    Task Delete(int meatId);

    IEnumerable<Meat> GetForRecipe(int recipeId);
}
