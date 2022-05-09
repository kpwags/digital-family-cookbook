namespace DigitalFamilyCookbook.Data.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IRecipeCategoryRepository _recipeCategoryRepository;
    private readonly IRecipeMeatRepository _recipeMeatRepository;
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IStepRepository _stepRepository;

    public RecipeRepository(ApplicationDbContext db,
        IRecipeCategoryRepository recipeCategoryRepository,
        IRecipeMeatRepository recipeMeatRepository,
        IIngredientRepository ingredientRepository,
        IStepRepository stepRepository)
    {
        _db = db;
        _recipeCategoryRepository = recipeCategoryRepository;
        _recipeMeatRepository = recipeMeatRepository;
        _ingredientRepository = ingredientRepository;
        _stepRepository = stepRepository;
    }

    public IEnumerable<Recipe> GetAll()
    {
        return _db.Recipes.Select(r => Recipe.FromDto(r));
    }

    public Recipe GetById(int recipeId)
    {
        var recipe = _db.Recipes.FirstOrDefault(r => r.RecipeId == recipeId);

        if (recipe is null)
        {
            throw new Exception("Recipe not found");
        }

        return Recipe.FromDto(recipe);
    }

    public async Task<Recipe> Add(Recipe recipe)
    {
        var dto = new RecipeDto
        {
            Id = Guid.NewGuid().ToString(),
            Name = recipe.Name,
            Description = recipe.Description,
            IsPublic = recipe.IsPublic,
            Servings = recipe.Servings,
            Source = recipe.Source,
            SourceUrl = recipe.SourceUrl,
            Time = recipe.Time,
            ActiveTime = recipe.ActiveTime,
            ImageUrl = recipe.ImageUrl,
            ImageUrlLarge = recipe.ImageUrlLarge,
            Calories = recipe.Calories,
            Carbohydrates = recipe.Carbohydrates,
            Sugar = recipe.Sugar,
            Fat = recipe.Fat,
            Protein = recipe.Protein,
            Fiber = recipe.Fiber,
            Cholesterol = recipe.Cholesterol,
            RecipeCategories = recipe.Categories.Select(c => new RecipeCategoryDto
            {
                Id = Guid.NewGuid().ToString(),
                CategoryId = c.CategoryId
            }).ToList(),
            RecipeMeats = recipe.Meats.Select(m => new RecipeMeatDto
            {
                Id = Guid.NewGuid().ToString(),
                MeatId = m.MeatId
            }).ToList(),
            Ingredients = recipe.Ingredients.Select(i => new IngredientDto
            {
                Id = Guid.NewGuid().ToString(),
                Name = i.Name,
                SortOrder = i.SortOrder,
            }).ToList(),
            Steps = recipe.Steps.Select(s => new StepDto
            {
                Id = Guid.NewGuid().ToString(),
                Direction = s.Direction,
                SortOrder = s.SortOrder,
            }).ToList()
        };

        _db.Recipes.Add(dto);

        await _db.SaveChangesAsync();

        return Recipe.FromDto(dto);
    }

    public async Task Update(Recipe recipe)
    {
        var dto = _db.Recipes.FirstOrDefault(r => r.RecipeId == recipe.RecipeId);

        if (dto is null)
        {
            throw new Exception("Recipe not found");
        }

        dto.Name = recipe.Name;
        dto.Description = recipe.Description;
        dto.IsPublic = recipe.IsPublic;
        dto.Servings = recipe.Servings;
        dto.Source = recipe.Source;
        dto.SourceUrl = recipe.SourceUrl;
        dto.Time = recipe.Time;
        dto.ActiveTime = recipe.ActiveTime;
        dto.ImageUrl = recipe.ImageUrl;
        dto.ImageUrlLarge = recipe.ImageUrlLarge;
        dto.Calories = recipe.Calories;
        dto.Carbohydrates = recipe.Carbohydrates;
        dto.Sugar = recipe.Sugar;
        dto.Fat = recipe.Fat;
        dto.Protein = recipe.Protein;
        dto.Fiber = recipe.Fiber;
        dto.Cholesterol = recipe.Cholesterol;

        _db.Recipes.Update(dto);

        await _db.SaveChangesAsync();

        await _ingredientRepository.DeleteForRecipe(recipe.RecipeId);
        await _stepRepository.DeleteForRecipe(recipe.RecipeId);
        await _recipeCategoryRepository.DeleteForRecipe(recipe.RecipeId);
        await _recipeMeatRepository.DeleteForMeat(recipe.RecipeId);

        await _recipeCategoryRepository.AddForRecipe(dto.RecipeId, recipe.Categories.Select(c => c.CategoryId).ToList());
        await _recipeMeatRepository.AddForRecipe(dto.RecipeId, recipe.Meats.Select(m => m.MeatId).ToList());
        await AddIngredients(dto.RecipeId, recipe.Ingredients);
        await AddSteps(dto.RecipeId, recipe.Steps);
    }

    public async Task Delete(int recipeId)
    {
        var recipe = _db.Recipes.FirstOrDefault(r => r.RecipeId == recipeId);

        if (recipe is null)
        {
            throw new Exception("Recipe not found");
        }

        await _ingredientRepository.DeleteForRecipe(recipeId);
        await _stepRepository.DeleteForRecipe(recipeId);
        await _recipeCategoryRepository.DeleteForRecipe(recipeId);
        await _recipeMeatRepository.DeleteForMeat(recipeId);

        _db.Recipes.Remove(recipe);

        await _db.SaveChangesAsync();
    }

    private async Task AddIngredients(int recipeId, IEnumerable<Ingredient> ingredients)
    {
        foreach (var ingredient in ingredients)
        {
            ingredient.RecipeId = recipeId;
            await _ingredientRepository.Add(ingredient);
        }
    }

    private async Task AddSteps(int recipeId, IEnumerable<Step> steps)
    {
        foreach (var step in steps)
        {
            step.RecipeId = recipeId;
            var newStep = await _stepRepository.Add(step);
        }
    }
}