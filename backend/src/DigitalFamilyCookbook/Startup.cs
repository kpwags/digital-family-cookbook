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

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(
                Configuration.GetConnectionString("Main"),
                dbOptions => dbOptions.MigrationsAssembly("DigitalFamilyCookbook")
            );
        });

        services.AddTokenAuthentication(_configuration.Auth.JwtSecret);

        services.AddIdentity<UserAccount, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

        services.AddMvc();

        services.AddMediatR(typeof(Startup));

        services.AddRepositories();
        services.AddServices();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "DigitalFamilyCookbook", Version = "v1" });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DigitalFamilyCookbook v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseGraphiQl("/graphql");

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
