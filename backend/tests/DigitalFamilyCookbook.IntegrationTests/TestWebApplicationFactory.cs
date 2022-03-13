using DigitalFamilyCookbook.Data.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalFamilyCookbook.IntegrationTests;

public class TestWebApplicationFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor is not null)
            {
                // remove link to database
                services.Remove(descriptor);
            }

            // add in memory database
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDFC");
            });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();

            using (var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
            {
                try
                {
                    context.Database.EnsureCreated();

                    TestPreparation.Seed(context);
                }
                catch
                {
                    throw;
                }
            }
        });
    }
}
