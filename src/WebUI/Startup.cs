using CaWorkshop.Application;
using CaWorkshop.Application.Infrastructure.Identity;
using CaWorkshop.Infrastructure;
using CaWorkshop.Infrastructure.Data;
using CaWorkshop.WebUI.Filters;
using CaWorkshop.WebUI.Services;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace CaWorkshop.WebUI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        services.AddInfrastructureServices(Configuration);

        services.AddApplicationServices();

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddControllersWithViews(options =>
            options.Filters.Add(new ApiExceptionFilterAttribute()));

        services.AddRazorPages();

        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddOpenApiDocument(configure =>
        {
            configure.Title = "CaWorkshop API";
            configure.AddSecurity("JWT", Enumerable.Empty<string>(),
                new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

            configure.OperationProcessors.Add(
                new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        });

        services.AddHealthChecks()
        .AddDbContextCheck<ApplicationDbContext>()
        .AddSmtpHealthCheck(options =>
        {
            options.Host = "localhost";
            options.Port = 25;
        });
        services.AddHealthChecksUI()
        .AddInMemoryStorage();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseSwaggerUi3(config =>
            config.DocumentPath = "/api/v1/specification.json");

        app.UseReDoc(config =>
        {
            config.Path = "/redoc";
            config.DocumentPath = "/api/v1/specification.json";
        });

        app.UseAuthentication();
        app.UseIdentityServer();
        app.UseAuthorization();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
            endpoints.MapFallbackToFile("index.html");
            endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            endpoints.MapHealthChecksUI();
        });
    }
}
