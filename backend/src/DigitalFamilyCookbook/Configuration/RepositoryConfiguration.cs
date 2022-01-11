using DigitalFamilyCookbook.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalFamilyCookbook.Configuration
{
    public static class RepositoryConfiguration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        }
    }
}