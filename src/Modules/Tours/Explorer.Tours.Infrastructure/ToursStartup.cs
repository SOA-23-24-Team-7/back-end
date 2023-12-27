using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.UseCases;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.API.Public.TourExecution;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Services;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.Mappers;
using Explorer.Tours.Core.UseCases;
using Explorer.Tours.Core.UseCases.Administration;
using Explorer.Tours.Core.UseCases.TourAuthoring;
using Explorer.Tours.Infrastructure.Database;
using Explorer.Tours.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
        services.AddScoped<ISubscriberService, SubscriberService>();
        services.AddScoped<IFacilityService, FacilityService>();

        services.AddScoped<ITourService, TourService>();

        services.AddScoped<ITourSearchService, TourSearchService>();

        services.AddScoped<IKeyPointService, KeyPointService>();

        services.AddScoped<IInternalKeyPointService, KeyPointService>();

        services.AddScoped<IReviewService, ReviewService>();

        services.AddScoped<ITourService, TourService>();

        services.AddScoped<IInternalTourService, TourService>();

        services.AddScoped<IPreferenceService, PreferenceService>();

        services.AddScoped<ITouristEquipmentService, TouristEquipmentService>();

        services.AddScoped<ITourExecutionSessionService, TourExecutionSessionService>();

        services.AddScoped<IInternalTourExecutionSessionService, TourExecutionSessionService>();

        services.AddScoped<ICampaignService, CampaignService>();

        services.AddScoped<ITouristPositionService, TouristPositionService>();

        services.AddScoped<IPublicKeyPointRequestService, PublicKeyPointRequestService>();

        services.AddScoped<IPublicFacilityRequestService, PublicFacilityRequestService>();

        services.AddScoped<INotificationService, NotificationService>();

        services.AddScoped<IPublicKeyPointService, PublicKeyPointService>();

        services.AddScoped<IShoppingCartService, ShoppingCartService>();

        services.AddScoped<IOrderItemService, OrderItemService>();

        services.AddScoped<ITourTokenService, TourTokenService>();

        services.AddScoped<IToursRecommendersService, TourRecommendersService>();

        services.AddScoped<IInternalNotificationService, NotificationService>();

        services.AddScoped<IMailingListScheduler, MailingListScheduler>();
     
        services.AddScoped<API.Public.ITourStatisticsService, Core.Domain.Services.TourStatisticsService>();
        

    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Equipment>), typeof(CrudDatabaseRepository<Equipment, ToursContext>));

        services.AddScoped(typeof(ISubscriberRepository), typeof(SubscriberDatabaseRepository));


        services.AddScoped(typeof(ICrudRepository<Facility>), typeof(CrudDatabaseRepository<Facility, ToursContext>));

        services.AddScoped(typeof(IKeyPointRepository), typeof(KeyPointDatabaseRepository));

        services.AddScoped(typeof(ICrudRepository<PublicKeyPointRequest>), typeof(CrudDatabaseRepository<PublicKeyPointRequest, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<PublicFacilityRequest>), typeof(CrudDatabaseRepository<PublicFacilityRequest, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<PublicKeyPointNotification>), typeof(CrudDatabaseRepository<PublicKeyPointNotification, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<PublicFacilityNotification>), typeof(CrudDatabaseRepository<PublicFacilityNotification, ToursContext>));
        services.AddScoped<IPublicKeyPointNotificationRepository, PublicKeyPointNotificationDatabaseRepository>();
        services.AddScoped<IPublicFacilityNotificationRepository, PublicFacilityNotificationDatabaseRepository>();

        services.AddScoped(typeof(ICrudRepository<Review>), typeof(CrudDatabaseRepository<Review, ToursContext>));
        services.AddScoped<IReviewRepository, ReviewDatabaseRepository>();

        services.AddScoped(typeof(ICrudRepository<Preference>), typeof(CrudDatabaseRepository<Preference, ToursContext>));
        services.AddScoped<IPreferenceRepository, PreferenceDatabaseRepository>();

        services.AddScoped(typeof(ICrudRepository<Tour>), typeof(CrudDatabaseRepository<Tour, ToursContext>));
        services.AddScoped(typeof(ITourRepository), typeof(TourRepository));

        services.AddScoped(typeof(ITourExecutionSessionRepository), typeof(TourExecutionSessionDatabaseRepository));
        services.AddScoped(typeof(ICampaignRepository), typeof(CampaignDatabaseRepository));
        services.AddScoped(typeof(ICrudRepository<TouristEquipment>), typeof(CrudDatabaseRepository<TouristEquipment, ToursContext>));

        services.AddScoped(typeof(ICrudRepository<TouristPosition>), typeof(CrudDatabaseRepository<TouristPosition, ToursContext>));
        services.AddScoped<ITouristPositionRepository, TouristPositionRepository>();

        services.AddScoped(typeof(ICrudRepository<PublicKeyPoint>), typeof(CrudDatabaseRepository<PublicKeyPoint, ToursContext>));

        services.AddDbContext<ToursContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("tours"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "tours")));
    }
}