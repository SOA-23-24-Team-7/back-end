using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.API.Internal;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Payments.Core.Mappers;
using Explorer.Payments.Core.UseCases;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Payments.Infrastructure.Database.Repositories;
using Explorer.Tours.API.Internal;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.UseCases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Payments.Infrastructure;

public static class PaymentsStartup
{
    public static IServiceCollection ConfigurePaymentsModule(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(PaymentsProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }

    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped(typeof(IShoppingCartRepository), typeof(ShoppingCartDatabaseRepository));
        services.AddScoped<IShoppingCartService, ShoppingCartService>();

        services.AddScoped<IOrderItemService, OrderItemService>();

        services.AddScoped<ITourTokenService, TourTokenService>();
        services.AddScoped<IInternalTourTokenService, TourTokenService>();

        services.AddScoped<ITourSaleService, TourSaleService>();

        services.AddScoped<IWalletService, WalletService>();

        services.AddScoped<IRecordService, RecordService>();
        services.AddScoped<ITransactionRecordService, TransactionRecordService>();

        services.AddScoped<IShoppingNotificationService, ShoppingNotificationService>();

        services.AddScoped<ICouponService, CouponService>();

        services.AddScoped<IBundleService, BundleService>();
       
        services.AddScoped<API.Public.ITourStatisticsService, Core.Domain.Services.TourStatisticsService>();

        services.AddScoped<IWishlistService, WishlistService>();
        services.AddScoped<IWishlistNotificationService, WishlistNotificationService>();
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<ShoppingCart>), typeof(CrudDatabaseRepository<ShoppingCart, PaymentsContext>));

        services.AddScoped(typeof(ICrudRepository<OrderItem>), typeof(CrudDatabaseRepository<OrderItem, PaymentsContext>));

        services.AddScoped(typeof(ICrudRepository<TourToken>), typeof(CrudDatabaseRepository<TourToken, PaymentsContext>));

        services.AddScoped(typeof(ITourSaleRepository), typeof(TourSaleDatabaseRepository));

        services.AddScoped(typeof(ICrudRepository<Wallet>), typeof(CrudDatabaseRepository<Wallet, PaymentsContext>));

        services.AddScoped(typeof(ICrudRepository<Record>), typeof(CrudDatabaseRepository<Record, PaymentsContext>));
        services.AddScoped(typeof(ICrudRepository<TransactionRecord>), typeof(CrudDatabaseRepository<TransactionRecord, PaymentsContext>));

        services.AddScoped(typeof(ICrudRepository<ShoppingNotification>), typeof(CrudDatabaseRepository<ShoppingNotification, PaymentsContext>));

        services.AddScoped(typeof(IShoppingNotificationRepository), typeof(ShoppingNotificationDatabaseRepository));
        services.AddScoped(typeof(IRecordRepository), typeof(RecordDatabaseRepository));
        services.AddScoped(typeof(ITransactionRecordRepository), typeof(TransactionRecordDatabaseRepository));

        services.AddScoped(typeof(ICouponRepository), typeof(CouponDatabaseRepository));
        services.AddScoped(typeof(ICrudRepository<Coupon>), typeof(CrudDatabaseRepository<Coupon, PaymentsContext>));

        services.AddScoped(typeof(IBundleRepository), typeof(BundleDatabaseRepository));
        services.AddScoped(typeof(IBundleRecordRepository), typeof(BundleRecordDatabaseRepository));

        services.AddScoped(typeof(ICrudRepository<Wishlist>), typeof(CrudDatabaseRepository<Wishlist, PaymentsContext>));
        services.AddScoped(typeof(ICrudRepository<WishlistNotification>), typeof(CrudDatabaseRepository<WishlistNotification, PaymentsContext>));

        services.AddDbContext<PaymentsContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("payments"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "payments")));
    }
}
