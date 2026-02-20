using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperHero.Application.Interfaces;
using SuperHero.Application.Notifications;
using SuperHero.Application.Services;
using SuperHero.Application.Settings;
using SuperHero.Infrastructure.Settings;

namespace SuperHero.Application;

public static class DependencyInjection
{
    public static void SetupSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
    }
    
    public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);
        
        services.AddDependencyRepositories();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        AddDependencyServices(services);
    }
    
    private static void AddDependencyServices(this IServiceCollection services)
    {
        services
            .AddScoped<INotificator, Notificator>();

        services
            .AddScoped<IHeroiService, HeroiService>()
            .AddScoped<ISuperpoderService, SuperpoderService>();
    }
}