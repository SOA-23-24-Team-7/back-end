using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Mappers;
using Explorer.Encounters.Core.UseCases;
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
    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IEncounterService, EncounterService>();
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Encounter.Core.Domain.Encounter>), typeof(CrudDatabaseRepository<Encounter.Core.Domain.Encounter, EncountersContext>));

        services.AddDbContext<EncountersContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("encounters"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "encounters")));
    }

}