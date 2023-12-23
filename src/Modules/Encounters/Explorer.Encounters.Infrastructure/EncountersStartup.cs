using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.API.Internal;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.Encounter;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Encounters.Core.Domain.Services;
using Explorer.Encounters.Core.Mappers;
using Explorer.Encounters.Core.UseCases;
using Explorer.Encounters.Infrastructure.Database;
using Explorer.Encounters.Infrastructure.Database.Repositories;
using Explorer.Payments.Infrastructure.Database.Repositories;
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
        services.AddScoped<ITouristProgressService, TouristProgressService>();
        services.AddScoped<ITourStatisticsService, TourStatisticsService>();
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Encounter>), typeof(CrudDatabaseRepository<Encounter, EncountersContext>));
        services.AddScoped(typeof(ICrudRepository<TouristProgress>), typeof(CrudDatabaseRepository<TouristProgress, EncountersContext>));
        services.AddScoped(typeof(ICrudRepository<SocialEncounter>), typeof(CrudDatabaseRepository<SocialEncounter, EncountersContext>));
        services.AddScoped<IEncounterRepository, EncounterDatabaseRepository>();
        services.AddScoped<IKeyPointEncounterRepository, KeyPointEncounterDatabaseRepository>();
        services.AddScoped<IInternalEncounterService, EncounterService>();
        services.AddScoped<IMiscEncounterRepository, MiscEncounterDatabaseRepository>();
        services.AddScoped<IHiddenLocationEncounterRepository, HiddenLocationEncounterDatabaseRepository>();
        services.AddScoped<ITouristProgressRepository, TouristProgressDatabaseRepository>();
        services.AddDbContext<EncountersContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("encounters"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "encounters")));
    }

}