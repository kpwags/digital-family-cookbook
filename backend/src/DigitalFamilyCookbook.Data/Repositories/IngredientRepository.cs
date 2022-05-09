namespace DigitalFamilyCookbook.Data.Repositories;

public class IngredientRepository : IIngredientRepository
{
    private readonly ApplicationDbContext _db;

    public IngredientRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Ingredient> Add(Ingredient ingredient)
    {
        var dto = new IngredientDto
        {
            Id = Guid.NewGuid().ToString(),
            Name = ingredient.Name,
            SortOrder = ingredient.SortOrder,
            RecipeId = ingredient.RecipeId,
        };

        _db.Ingredients.Add(dto);

        await _db.SaveChangesAsync();

        return Ingredient.FromDto(dto);
    }

    public async Task Delete(int ingredientId)
    {
        var ingredient = _db.Ingredients.FirstOrDefault(i => i.IngredientId == ingredientId);

        if (ingredient is null)
        {
            throw new Exception("Ingredient not found");
        }

        _db.Ingredients.Remove(ingredient);

        await _db.SaveChangesAsync();
    }

    public async Task DeleteRange(List<int> ingredientIds)
    {
        var ingredients = _db.Ingredients.Where(i => ingredientIds.Contains(i.IngredientId));

        _db.Ingredients.RemoveRange(ingredients);

        await _db.SaveChangesAsync();
    }

    public async Task DeleteForRecipe(int recipeId)
    {
        var ingredients = _db.Ingredients.Where(i => i.RecipeId == recipeId);

        _db.Ingredients.RemoveRange(ingredients);

        await _db.SaveChangesAsync();
    }
}