using DigitalFamilyCookbook.Core.Configuration;
using DigitalFamilyCookbook.Data.Database;
using DigitalFamilyCookbook.IntegrationTests.Database;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace DigitalFamilyCookbook.IntegrationTests.Fixtures;

public static class ApplicationFactory
{
    public static WebApplicationFactory<Startup> CreateAppFactory(string dbName = "")
    {
        if (dbName == "")
        {
            dbName = Guid.NewGuid().ToString();
        }

        var appFactory = new WebApplicationFactory<Startup>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
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
                        options.UseInMemoryDatabase(dbName);
                    });

                    var sp = services.BuildServiceProvider();

                    using var scope = sp.CreateScope();

                    using (var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        try
                        {
                            context.Database.EnsureCreated();

                            await DatabaseSeeder.Seed(context);
                        }
                        catch
                        {
                            throw;
                        }
                    }
                });
            });

        return appFactory;
    }

    public static HttpClient CreateClient(UserAccountDto? user = null, WebApplicationFactory<Startup>? factory = null)
    {
        if (factory is null)
        {
            factory = CreateAppFactory();
        }

        var client = factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            }
        );

        if (user is not null)
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.test.json");

            var appSettings = new ConfigurationBuilder()
                .AddJsonFile(configPath)
                .Build();

            var config = new DigitalFamilyCookbookConfiguration();

            appSettings.Bind(config);

            var token = MockAuthToken.GenerateToken(user, config.Auth);

            client.DefaultRequestHeaders.Add("Authorization", $"bearer {token}");
        }

        return client;
    }
}
