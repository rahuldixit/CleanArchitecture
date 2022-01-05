using CaWorkshop.Application.Common.Interfaces;
using CaWorkshop.Application.Infrastructure.Identity;
using CaWorkshop.Application.Infrastructure.Messaging;
using CaWorkshop.Infrastructure.Data;
using CaWorkshop.Infrastructure.Identity;
using CaWorkshop.Infrastructure.Messaging;
using CaWorkshop.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CaWorkshop.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
            options
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .AddInterceptors(
                    ActivatorUtilities.CreateInstance<AuditEntitiesSaveChangesInterceptor>(sp)));

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

        services.AddAuthentication()
            .AddIdentityServerJwt();

        services.AddScoped<IMessagingService, SmtpMessagingService>();

        services.AddScoped<IIdentityService, IdentityService>();

        return services;
    }
}
