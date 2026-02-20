using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperHero.Domain.Interfaces.Repositories;
using SuperHero.Infrastructure.Data.Context;
using SuperHero.Infrastructure.Data.Repositories;

namespace SuperHero.Infrastructure.Settings;

public static class DependencyInjection
{
    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            options.UseMySql(connectionString, serverVersion);
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });

        services.AddScoped<BaseApplicationDbContext>(serviceProvider =>
        {
            return serviceProvider.GetRequiredService<ApplicationDbContext>();
        });
    }

    public static void AddDependencyRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IHeroiRepository, HeroiRepository>();
        services
            .AddScoped<ISuperpoderRepository, SuperpoderRepository>();
    }

    public static void UseMigrations(this IApplicationBuilder app, IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.Migrate();
    }
}