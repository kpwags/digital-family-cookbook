using DigitalFamilyCookbook.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalFamilyCookbook.Configuration
{
    public static class ServicesConfiguration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}