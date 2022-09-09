using DataPopulator.Configuration;
using DataPopulator.Data;
using DigitalFamilyCookbook.Data.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace DataPopulator; // Note: actual namespace depends on the project name.

public class Program
{
    private static DataPopulatorConfiguration _config = new DataPopulatorConfiguration();

    static async Task Main(string[] args)
    {
        LoadSettings();

        WriteLine("Please enter what you would like to create");
        WriteLine("1. Users");
        WriteLine("2. Recipes");

        int choice = int.Parse(ReadLine() ?? "0");

        switch (choice)
        {
            case 1:
                WriteLine("Not yet implemented");
                break;
            case 2:
                await ProcessRecipeCreation();
                break;
            default:
                WriteLine("Invalid Choice");
                return;
        }
    }

    private static async Task ProcessRecipeCreation()
    {
        WriteLine("Please enter the user ID: ");

        string UserAccountId = ReadLine() ?? "";

        WriteLine("Please enter the number of recipes to create: ");

        string userCountInput = ReadLine() ?? "0";

        WriteLine("Please enter the starting #: ");

        string startingNumberInput = ReadLine() ?? "0";

        if (int.TryParse(userCountInput, out int userCount) && int.TryParse(startingNumberInput, out int startingInput))
        {
            var categories = await DataService.GetCategories();
            var meats = await DataService.GetMeats();

            for (int x = 0; x < userCount; x += 1)
            {
                var recipe = new RecipeDto
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = $"Recipe {x + startingInput}",
                    Description = $"Recipe Description {x + startingInput}",
                    IsPublic = RandomBoolean(),
                    Servings = RandomInteger(1, 10),
                    Time = RandomInteger(15, 60),
                    ActiveTime = RandomInteger(30, 75),
                    Calories = RandomInteger(100, 600),
                    Carbohydrates = RandomInteger(5, 30),
                    Sugar = RandomInteger(5, 30),
                    Fat = RandomInteger(5, 30),
                    Protein = RandomInteger(5, 30),
                    Fiber = RandomInteger(5, 30),
                    Cholesterol = RandomInteger(10, 50),
                    UserAccountId = UserAccountId,
                };

                var recipeId = await DataService.AddRecipe(recipe);

                var ingredientCount = RandomInteger(1, 10);

                for (int y = 0; y < ingredientCount; y += 1)
                {
                    var ingredient = new IngredientDto
                    {
                        RecipeId = recipeId,
                        Name = $"Ingredient {y + 1}",
                        SortOrder = y + 1,
                        Id = Guid.NewGuid().ToString(),
                    };

                    await DataService.AddIngredient(ingredient);
                }

                var stepCount = RandomInteger(1, 10);

                for (int z = 0; z < stepCount; z += 1)
                {
                    var step = new StepDto
                    {
                        RecipeId = recipeId,
                        Direction = $"Step {z + 1}",
                        SortOrder = z + 1,
                        Id = Guid.NewGuid().ToString(),
                    };

                    await DataService.AddStep(step);
                }

                var categoryCount = RandomInteger(1, categories.Count());
                if (categoryCount > 3)
                {
                    categoryCount = 3;
                }

                var recipeCategories = GetRandomSubset(
                    categories.Select(c => c.CategoryId).ToList(),
                    categoryCount
                );

                foreach (var categoryId in recipeCategories)
                {
                    await DataService.AddRecipeCategory(recipeId, categoryId);
                }

                var meatCount = RandomInteger(1, meats.Count());
                if (meatCount > 3)
                {
                    meatCount = 3;
                }

                var recipeMeats = GetRandomSubset(
                    meats.Select(m => m.MeatId).ToList(),
                    categoryCount
                );

                foreach (var meatId in recipeMeats)
                {
                    await DataService.AddRecipeMeat(recipeId, meatId);
                }
            }
        }
        else
        {
            WriteLine("Invalid Input");
            return;
        }
    }

    private static void LoadSettings()
    {

        // Build configuration
        var baseDirectory = Directory.GetParent(AppContext.BaseDirectory);

        if (baseDirectory is null)
        {
            throw new Exception("Unable to read base directory");
        }

        var configuration = new ConfigurationBuilder()
            .SetBasePath(baseDirectory.FullName)
            .AddJsonFile("appsettings.json", false)
            .AddJsonFile("appsettings.local.json", false)
            .Build();

        configuration.Bind(_config);

        DataService.ConnectionString = _config.ConnectionStrings.Main;
    }

    public static int RandomInteger(int min = 1, int max = 100)
    {
        Random random = new Random(Guid.NewGuid().GetHashCode());

        return random.Next(min, max);
    }

    public static bool RandomBoolean()
    {
        Random random = new Random(Guid.NewGuid().GetHashCode());

        return random.Next(0, 100) % 2 == 0;
    }

    public static List<int> GetRandomSubset(List<int> OriginalList, int outCount)
    {
        var ids = new List<int>();

        for (int x = 0; x < outCount; x += 1)
        {
            var randomId = RandomInteger(1, OriginalList.Count);

            ids.Add(OriginalList[randomId]);

            OriginalList.RemoveAt(randomId);
        }

        return ids;
    }
}
