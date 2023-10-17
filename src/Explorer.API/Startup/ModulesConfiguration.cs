using Explorer.Tours.Infrastructure;
using Explorer.Stakeholders.Infrastructure;
using Explorer.Blog.Infrastructure;

namespace Explorer.API.Startup;

public static class ModulesConfiguration
{
    public static IServiceCollection RegisterModules(this IServiceCollection services)
    {
        services.ConfigureStakeholdersModule();
        services.ConfigureToursModule();
        services.ConfigureBlogModule();

        return services;
    }
}