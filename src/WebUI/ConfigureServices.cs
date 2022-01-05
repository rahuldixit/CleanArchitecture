using CaWorkshop.Application.Infrastructure.Identity;
using CaWorkshop.WebUI.Filters;
using CaWorkshop.WebUI.Services;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace CaWorkshop.WebUI
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebUIServices(this IServiceCollection services)
        {
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

            services.AddLogging(configure
                => configure.AddSeq());

            return services;
        }
    }
}
