using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.Core.Mappers;
using Explorer.Encounters.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Encounters.Infrastructure;

public static class EncountersStartup
{
    public static IServiceCollection ConfigureEncountersModule(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(EncountersProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        
    }

    private static void SetupCore(IServiceCollection services)
    {
        services.AddDbContext<EncountersContext>(opt =>
    opt.UseNpgsql(DbConnectionStringBuilder.Build("encounters"),
        x => x.MigrationsHistoryTable("__EFMigrationsHistory", "encounters")));
    }
}