using System.Text.Json;
using DigitalFamilyCookbook.Data.Models;
using Microsoft.EntityFrameworkCore;

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

    public IEnumerable<Recipe> GetAll(bool includePrivate = true)
    {
        return _db.Recipes
            .Where(r => r.IsPublic || includePrivate)
            .Include(r => r.UserAccount)
            .Select(r => Recipe.FromDto(r));
    }

    public Recipe GetById(int recipeId)
    {
        var recipe = _db.Recipes
            .AsSplitQuery()
            .Include(r => r.UserAccount)
            .Include(r => r.Ingredients)
            .Include(r => r.Steps)
            .FirstOrDefault(r => r.RecipeId == recipeId);

        if (recipe is null)
        {
            throw new Exception("Recipe not found");
        }

        return Recipe.FromDto(recipe);
    }

    public async Task<Recipe> Add(Recipe recipe)
    {
        if (_db.Recipes.Any(r => r.Name.ToLower() == recipe.Name.ToLower()))
        {
            throw new Exception($"A recipe with the name '{recipe.Name}' already exists");
        }

        var dto = new RecipeDto
        {
            Id = Guid.NewGuid().ToString(),
            Name = recipe.Name.Trim(),
            Description = (recipe.Description ?? "").Trim(),
            IsPublic = recipe.IsPublic,
            Servings = recipe.Servings,
            Source = (recipe.Source ?? "").Trim(),
            SourceUrl = (recipe.SourceUrl ?? "").Trim(),
            Time = recipe.Time,
            ActiveTime = recipe.ActiveTime,
            ImageUrl = (recipe.ImageUrl ?? "").Trim(),
            ImageUrlLarge = (recipe.ImageUrlLarge ?? "").Trim(),
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
                CategoryId = c.CategoryId,
                Category = _db.Categories.First(dbc => dbc.CategoryId == c.CategoryId)
            }).ToList(),
            RecipeMeats = recipe.Meats.Select(m => new RecipeMeatDto
            {
                Id = Guid.NewGuid().ToString(),
                MeatId = m.MeatId,
                Meat = _db.Meats.First(dbm => dbm.MeatId == m.MeatId),
            }).ToList(),
            Ingredients = recipe.Ingredients.Select(i => new IngredientDto
            {
                Id = Guid.NewGuid().ToString(),
                Name = i.Name.Trim(),
                SortOrder = i.SortOrder,
            }).ToList(),
            Steps = recipe.Steps.Select(s => new StepDto
            {
                Id = Guid.NewGuid().ToString(),
                Direction = s.Direction.Trim(),
                SortOrder = s.SortOrder,
            }).ToList(),
            UserAccountId = recipe.UserAccountId,
            UserAccount = _db.UserAccounts.First(dbu => dbu.Id == recipe.UserAccount.Id),
        };

        _db.Recipes.Add(dto);

        await _db.SaveChangesAsync();

        return Recipe.FromDto(dto);
    }

    public async Task Update(Recipe recipe)
    {

        if (_db.Recipes.Any(r => r.Name.ToLower() == recipe.Name.ToLower() && r.RecipeId != recipe.RecipeId))
        {
            throw new Exception($"A recipe with the name '{recipe.Name}' already exists");
        }

        var dto = _db.Recipes.FirstOrDefault(r => r.RecipeId == recipe.RecipeId);

        if (dto is null)
        {
            throw new Exception("Recipe not found");
        }

        await _ingredientRepository.DeleteForRecipe(recipe.RecipeId);
        await _stepRepository.DeleteForRecipe(recipe.RecipeId);
        await _recipeCategoryRepository.DeleteForRecipe(recipe.RecipeId);
        await _recipeMeatRepository.DeleteForRecipe(recipe.RecipeId);

        dto.Name = recipe.Name.Trim();
        dto.Description = (recipe.Description ?? "").Trim();
        dto.IsPublic = recipe.IsPublic;
        dto.Servings = recipe.Servings;
        dto.Source = recipe.Source.Trim();
        dto.SourceUrl = recipe.SourceUrl.Trim();
        dto.Time = recipe.Time;
        dto.ActiveTime = recipe.ActiveTime;
        dto.ImageUrl = recipe.ImageUrl.Trim();
        dto.ImageUrlLarge = recipe.ImageUrlLarge.Trim();
        dto.Calories = recipe.Calories;
        dto.Carbohydrates = recipe.Carbohydrates;
        dto.Sugar = recipe.Sugar;
        dto.Fat = recipe.Fat;
        dto.Protein = recipe.Protein;
        dto.Fiber = recipe.Fiber;
        dto.Cholesterol = recipe.Cholesterol;
        dto.RecipeCategories = recipe.Categories.Select(c => new RecipeCategoryDto
        {
            Id = Guid.NewGuid().ToString(),
            CategoryId = c.CategoryId,
            Category = _db.Categories.First(dbc => dbc.CategoryId == c.CategoryId)
        }).ToList();
        dto.RecipeMeats = recipe.Meats.Select(m => new RecipeMeatDto
        {
            Id = Guid.NewGuid().ToString(),
            MeatId = m.MeatId,
            Meat = _db.Meats.First(dbm => dbm.MeatId == m.MeatId),
        }).ToList();
        dto.Ingredients = recipe.Ingredients.Select(i => new IngredientDto
        {
            Id = Guid.NewGuid().ToString(),
            Name = i.Name.Trim(),
            SortOrder = i.SortOrder,
        }).ToList();
        dto.Steps = recipe.Steps.Select(s => new StepDto
        {
            Id = Guid.NewGuid().ToString(),
            Direction = s.Direction.Trim(),
            SortOrder = s.SortOrder,
        }).ToList();
        dto.DateUpdated = DateTime.Now;

        _db.Recipes.Update(dto);

        await _db.SaveChangesAsync();
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

    public async Task DeleteRecipeImage(int recipeId)
    {
        var recipe = _db.Recipes.FirstOrDefault(r => r.RecipeId == recipeId);

        if (recipe is null)
        {
            throw new Exception("Recipe not found");
        }

        recipe.ImageUrl = "";
        recipe.ImageUrlLarge = "";

        _db.Recipes.Update(recipe);

        await _db.SaveChangesAsync();
    }

    public IEnumerable<Recipe> GetRecipesForUser(string userAccountId, bool includePrivate)
    {
        return _db.Recipes
            .Where(r => r.UserAccountId == userAccountId && (includePrivate || r.IsPublic))
            .Include(r => r.UserAccount)
            .Select(r => Recipe.FromDto(r));
    }

    public (IEnumerable<Recipe> recipes, int totalRecipes) GetRecipesForUserPaginated(string userAccountId, bool includePrivate, int currentPage = 1, int recipesPerPage = 10)
    {
        var data = _db.Recipes
            .Where(r => r.UserAccountId == userAccountId && (includePrivate || r.IsPublic))
            .OrderBy(r => r.Name)
            .Skip(currentPage == 1 ? 0 : (currentPage - 1) * recipesPerPage)
            .Include(r => r.UserAccount)
            .AsSplitQuery()
            .Take(recipesPerPage);

        var recipes = new List<Recipe>();

        foreach (var recipeDto in data)
        {
            var recipe = AddCategoriesAndMeatsToRecipe(recipeDto);
            recipes.Add(recipe);
        }

        var recipeCount = _db.Recipes
            .Count(r => r.UserAccountId == userAccountId);

        return (recipes, recipeCount);
    }

    public IEnumerable<Recipe> GetRecipesForCategory(int categoryId, bool includePrivate)
    {
        return _db.Recipes
                .Include(r => r.RecipeCategories)
                .Where(r => r.RecipeCategories.Select(rc => rc.CategoryId).Contains(categoryId) && (includePrivate || r.IsPublic))
                .Select(r => Recipe.FromDto(r));
    }

    public (IEnumerable<Recipe> recipes, int totalRecipes) GetRecipesForCategoryPaginated(int categoryId, bool includePrivate, int currentPage = 1, int recipesPerPage = 10)
    {
        var data = _db.Recipes
            .Where(r => r.RecipeCategories.Select(rc => rc.CategoryId).Contains(categoryId) && (includePrivate || r.IsPublic))
            .OrderBy(r => r.Name)
            .Skip(currentPage == 1 ? 0 : (currentPage - 1) * recipesPerPage)
            .Include(r => r.RecipeCategories)
            .Include(r => r.RecipeMeats)
            .AsSplitQuery()
            .Take(recipesPerPage);

        var recipes = new List<Recipe>();

        foreach (var recipeDto in data)
        {
            var recipe = AddCategoriesAndMeatsToRecipe(recipeDto);
            recipes.Add(recipe);
        }

        var recipeCount = _db.Recipes
            .Include(r => r.RecipeCategories)
            .Count(r => r.RecipeCategories.Select(rc => rc.CategoryId).Contains(categoryId));

        return (recipes, recipeCount);
    }

    public IEnumerable<Recipe> GetRecipesForMeat(int meatId, bool includePrivate)
    {
        return _db.Recipes
            .Include(r => r.RecipeMeats)
            .Where(r => r.RecipeMeats.Select(rm => rm.MeatId).Contains(meatId) && (includePrivate || r.IsPublic))
            .Select(r => Recipe.FromDto(r));
    }

    public (IEnumerable<Recipe> recipes, int totalRecipes) GetRecipesForMeatPaginated(int meatId, bool includePrivate, int currentPage = 1, int recipesPerPage = 10)
    {
        var data = _db.Recipes
            .Where(r => r.RecipeMeats.Select(rc => rc.MeatId).Contains(meatId) && (includePrivate || r.IsPublic))
            .OrderBy(r => r.Name)
            .Skip(currentPage == 1 ? 0 : (currentPage - 1) * recipesPerPage)
            .Include(r => r.RecipeMeats)
            .Include(r => r.RecipeCategories)
            .AsSplitQuery()
            .Take(recipesPerPage);

        var recipes = new List<Recipe>();

        foreach (var recipeDto in data)
        {
            var recipe = AddCategoriesAndMeatsToRecipe(recipeDto);
            recipes.Add(recipe);
        }

        var recipeCount = _db.Recipes
            .Include(r => r.RecipeMeats)
            .Count(r => r.RecipeMeats.Select(rm => rm.MeatId).Contains(meatId));

        return (recipes, recipeCount);
    }

    public (IEnumerable<Recipe> recipes, int totalRecipes) GetAllRecipesPaginated(bool includePrivate, int currentPage = 1, int recipesPerPage = 10)
    {
        var allRecipes = _db.Recipes.Include(r => r.UserAccount);

        var data = allRecipes
            .Where(r =>  includePrivate || r.IsPublic)
            .OrderBy(r => r.Name)
            .Skip(currentPage == 1 ? 0 : (currentPage - 1) * recipesPerPage)
            .Include(r => r.UserAccount)
            .Include(r => r.RecipeCategories)
            .Include(r => r.RecipeMeats)
            .AsSplitQuery()
            .Take(recipesPerPage);

        var recipes = new List<Recipe>();

        foreach (var recipeDto in data)
        {
            var recipe = AddCategoriesAndMeatsToRecipe(recipeDto);
            recipes.Add(recipe);
        }

        return (recipes, allRecipes.Count());
    }

    public async Task MarkRecipeAsFavorite(string userAccountId, int recipeId)
    {
        var recipe = _db.Recipes.FirstOrDefault(r => r.RecipeId == recipeId);
        var user = _db.UserAccounts.FirstOrDefault(u => u.Id == userAccountId);

        if (recipe is not null && user is not null)
        {
            var favorite = new RecipeFavoriteDto()
            {
                UserAccount = user,
                Recipe = recipe,
            };

            _db.RecipeFavorites.Add(favorite);

            await _db.SaveChangesAsync();
        }
    }

    public async Task RemoveRecipeAsFavorite(string userAccountId, int recipeId)
    {
        var favorite = _db.RecipeFavorites.FirstOrDefault(rf => rf.UserAccountId == userAccountId && rf.RecipeId == recipeId);

        if (favorite is not null)
        {
            _db.RecipeFavorites.Remove(favorite);

            await _db.SaveChangesAsync();
        }
    }

    public bool IsRecipeFavoriteForUser(string userAccountId, int recipeId)
    {
        return _db.RecipeFavorites.Any(rf => rf.UserAccountId == userAccountId && rf.RecipeId == recipeId);
    }

    public (IEnumerable<Recipe> recipes, int totalRecipes) GetFavoriteRecipesForUserPaginated(string userAccountId, int currentPage = 1, int recipesPerPage = 10)
    {
        var data = _db.Recipes
            .Where(r => r.RecipeFavorites.Any(rf => rf.UserAccountId == userAccountId && rf.RecipeId == r.RecipeId))
            .OrderBy(r => r.Name)
            .Skip(currentPage == 1 ? 0 : (currentPage - 1) * recipesPerPage)
            .Include(r => r.UserAccount)
            .Include(r => r.RecipeFavorites)
            .Include(r => r.RecipeMeats)
            .Include(r => r.RecipeCategories)
            .AsSplitQuery()
            .Take(recipesPerPage);

        var recipes = new List<Recipe>();

        foreach (var recipeDto in data)
        {
            var recipe = AddCategoriesAndMeatsToRecipe(recipeDto);
            recipes.Add(recipe);
        }

        var recipeCount = _db.Recipes
            .Include(r => r.RecipeFavorites)
            .Count(r => r.RecipeFavorites.Any(rf => rf.UserAccountId == userAccountId && rf.RecipeId == r.RecipeId));

        return (recipes, recipeCount);
    }

    public IEnumerable<Recipe> GetRecentRecipes(int count, bool includePrivate)
    {
        var data = _db.Recipes
            .Where(r => includePrivate || r.IsPublic)
            .OrderByDescending(r => r.DateCreated)
            .Include(r => r.UserAccount)
            .Include(r => r.RecipeFavorites)
            .Include(r => r.RecipeMeats)
            .Include(r => r.RecipeCategories)
            .AsSplitQuery()
            .Take(count);

        var recipes = new List<Recipe>();

        foreach (var recipeDto in data)
        {
            var recipe = AddCategoriesAndMeatsToRecipe(recipeDto);
            recipes.Add(recipe);
        }

        return recipes;
    }

    public IEnumerable<Recipe> GetMostFavoritedRecipes(int count, bool includePrivate)
    {
        var favoriteRecipes = _db.RecipeFavorites
            .Include(rf => rf.Recipe)
            .Where(rf => rf.Recipe.IsPublic || includePrivate)
            .ToList()
            .GroupBy(rf => rf.RecipeId)
            .Select(r => new RecipeGrouping() { RecipeId = r.Key, RecipeCount = r.Count() })
            .OrderByDescending(r => r.RecipeCount)
            .Take(count);

        var recipeIds = favoriteRecipes
            .Take(count)
            .Select(rf => rf.RecipeId);

        var data = _db.Recipes
            .Where(r => recipeIds.Contains(r.RecipeId))
            .Include(r => r.UserAccount)
            .Include(r => r.RecipeFavorites)
            .Include(r => r.RecipeMeats)
            .Include(r => r.RecipeCategories)
            .AsSplitQuery();

        var recipes = new List<Recipe>();

        foreach (var recipeDto in data)
        {
            var recipe = AddCategoriesAndMeatsToRecipe(recipeDto);
            recipes.Add(recipe);
        }

        var recipesOutput = new List<Recipe>();

        foreach (var recipe in favoriteRecipes)
        {
            var rec = recipes.FirstOrDefault(r => r.RecipeId == recipe.RecipeId);
            if (rec is not null)
            {
                recipesOutput.Add(rec);
            }
        }

        return recipesOutput;
    }

    public (IEnumerable<Recipe> recipes, int totalRecipes) SearchRecipesPaginated(string keywords, bool includePrivate, int currentPage = 1, int recipesPerPage = 10)
    {
        var data = _db.Recipes
            .Where(r => (includePrivate || r.IsPublic) && (r.Name.ToLower().Contains(keywords.ToLower()) || (r.Description ?? "").ToLower().Contains(keywords.ToLower())))
            .OrderBy(r => r.Name)
            .Skip(currentPage == 1 ? 0 : (currentPage - 1) * recipesPerPage)
            .Include(r => r.UserAccount)
            .Include(r => r.RecipeCategories)
            .Include(r => r.RecipeMeats)
            .AsSplitQuery()
            .Take(recipesPerPage);

        var recipes = new List<Recipe>();

        foreach (var recipeDto in data)
        {
            var recipe = AddCategoriesAndMeatsToRecipe(recipeDto);
            recipes.Add(recipe);
        }

        var recipeCount = _db.Recipes
            .Count(r => r.Name.ToLower().Contains(keywords.ToLower()) || (r.Description ?? "").ToLower().Contains(keywords.ToLower()));

        return (recipes, recipeCount);
    }

    public IEnumerable<Recipe> QuickSearchRecipes(string keywords, bool includePrivate, int count = 10)
    {
        var recipes = _db.Recipes
            .Where(r => (includePrivate || r.IsPublic) && (r.Name.ToLower().Contains(keywords.ToLower()) || (r.Description ?? "").ToLower().Contains(keywords.ToLower())))
            .OrderBy(r => r.Name)
            .Take(count);

        return recipes.Select(r => Recipe.FromDto(r));
    }

    private Recipe AddCategoriesAndMeatsToRecipe(RecipeDto dto)
    {
        var recipe = Recipe.FromDto(dto);

        recipe.Categories = GetRecipeCategories(dto);
        recipe.Meats = GetRecipeMeats(dto);

        return recipe;
    }

    private IEnumerable<Category> GetRecipeCategories(RecipeDto recipe)
    {
        var categoryIds = recipe.RecipeCategories.Select(rc => rc.CategoryId);

        return _db.Categories
            .Where(c => categoryIds.Contains(c.CategoryId))
            .Select(c => Category.FromDto(c));
    }

    private IEnumerable<Meat> GetRecipeMeats(RecipeDto recipe)
    {
        var meatIds = recipe.RecipeMeats.Select(rm => rm.MeatId);

        return _db.Meats
            .Where(m => meatIds.Contains(m.MeatId))
            .Select(m => Meat.FromDto(m));
    }
}