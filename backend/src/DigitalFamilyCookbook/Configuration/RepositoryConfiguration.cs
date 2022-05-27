using DigitalFamilyCookbook.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalFamilyCookbook.Configuration
{
    public static class RepositoryConfiguration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IMeatRepository, MeatRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IRecipeCategoryRepository, RecipeCategoryRepository>();
            services.AddScoped<IRecipeMeatRepository, RecipeMeatRepository>();
            services.AddScoped<IStepRepository, StepRepository>();
            services.AddScoped<ISystemRepository, SystemRepository>();
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();
        }
    }
}