using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Mappers;
using Explorer.Tours.Core.UseCases;
using Explorer.Tours.Core.UseCases.Administration;
using Explorer.Tours.Core.UseCases.TourAuthoring;
using Explorer.Tours.Core.UseCases;
using Explorer.Tours.Infrastructure.Database;
using Explorer.Tours.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Infrastructure.Database.Repositories;

namespace Explorer.Tours.Infrastructure;

public static class ToursStartup
{
    public static IServiceCollection ConfigureToursModule(this IServiceCollection services)
    {
        // Registers all profiles since it works on the assembly
        services.AddAutoMapper(typeof(ToursProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }

    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IEquipmentService, EquipmentService>();

        services.AddScoped<IFacilityService, FacilityService>();
      
        services.AddScoped<ITourService, TourService>();
      
        services.AddScoped<IKeyPointService, KeyPointService>();

        services.AddScoped<IReviewService, ReviewService>();

        services.AddScoped<ITourService, TourService>();

        services.AddScoped<IPreferenceService, PreferenceService>();

        services.AddScoped<IPublicKeyPointRequestService, PublicKeyPointRequestService>();
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Equipment>), typeof(CrudDatabaseRepository<Equipment, ToursContext>));

        services.AddScoped(typeof(ICrudRepository<Facility>), typeof(CrudDatabaseRepository<Facility, ToursContext>));
      
        services.AddScoped(typeof(IKeyPointRepository), typeof(KeyPointDatabaseRepository));

        services.AddScoped(typeof(ICrudRepository<PublicKeyPointRequest>), typeof(CrudDatabaseRepository<PublicKeyPointRequest, ToursContext>));

        services.AddScoped(typeof(ICrudRepository<Review>), typeof(CrudDatabaseRepository<Review, ToursContext>));
        services.AddScoped<IReviewRepository, ReviewDatabaseRepository>();

        services.AddScoped(typeof(ICrudRepository<Preference>), typeof(CrudDatabaseRepository<Preference, ToursContext>));
        services.AddScoped<IPreferenceRepository, PreferenceDatabaseRepository>();

        services.AddScoped(typeof(ICrudRepository<Tour>), typeof(CrudDatabaseRepository<Tour, ToursContext>));
        services.AddScoped(typeof(ITourRepository), typeof(TourRepository));

        services.AddDbContext<ToursContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("tours"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "tours")));
    }
}