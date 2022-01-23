using DigitalFamilyCookbook.Configuration;
using DigitalFamilyCookbook.Core.Configuration;
using DigitalFamilyCookbook.Data.Database;
using GraphiQl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DigitalFamilyCookbook;


public class Startup
{
    public IConfiguration Configuration { get; }

    private DigitalFamilyCookbookConfiguration _configuration = new DigitalFamilyCookbookConfiguration();

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;

        configuration.Bind(_configuration);
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(_configuration);

        services.AddCors(options =>
        {
            options.AddPolicy(name: "Development", builder =>
            {
                builder.WithOrigins(_configuration.CorsAllowedOrigins.ToArray())
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(
                Configuration.GetConnectionString("Main"),
                dbOptions => dbOptions.MigrationsAssembly("DigitalFamilyCookbook")
            );
        });

        services.AddIdentity<UserAccountDto, RoleTypeDto>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddTokenAuthentication(_configuration.Auth.JwtSecret);

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

        services.AddMvc();

        services.AddMediatR(typeof(Startup));

        services.AddRepositories();
        services.AddServices();

        services.AddHttpContextAccessor();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("Development");

        app.UseGraphiQl("/graphql");

        app.UseMiddleware<DigitalFamilyCookbook.Helpers.JwtMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
