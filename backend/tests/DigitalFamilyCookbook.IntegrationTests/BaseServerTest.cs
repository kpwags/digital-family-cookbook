using DigitalFamilyCookbook.Core.Configuration;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DigitalFamilyCookbook.IntegrationTests;

public class BaseServerTest<T> : IClassFixture<T>, IDisposable where T : BaseWebApplicationFactory
{
    protected readonly T _fixture;

    public BaseServerTest(T fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _fixture.Output = output;
    }

    public HttpClient CreateClient(UserAccountDto? user = null)
    {
        var client = _fixture.CreateClient(
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

    public void Dispose()
    {
        _fixture.Output = null;
    }
}
