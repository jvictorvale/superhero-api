using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;

namespace SuperHero.Api.Configuration;

public static class SwaggerConfiguration
{
    public static void AddSwagger(this IServiceCollection services)
    {
        var contact = new OpenApiContact
        {
            Name = "João Victor Vale",
            Email = "joaovictorvale.dev@gmail.com\n",
            Url = new Uri("https://github.com/jvictorvale")
        };
        
        var license = new OpenApiLicense
        {
            Name = "Free License",
            Url = new Uri("https://github.com/jvictorvale")
        };
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Version = "v1",
                Title = "SuperHero API",
                Contact = contact,
                License = license
            });

            options.OrderActionsBy(a => a.GroupName);
        });
    }
}