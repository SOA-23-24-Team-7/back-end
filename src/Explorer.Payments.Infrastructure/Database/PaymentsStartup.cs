

using Explorer.Payments.Core.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Payments.Infrastructure.Database;

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

    }

    private static void SetupInfrastructure(IServiceCollection services)
    {

    }
}
