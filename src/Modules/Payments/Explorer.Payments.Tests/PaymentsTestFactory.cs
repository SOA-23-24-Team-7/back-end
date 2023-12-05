using Explorer.BuildingBlocks.Tests;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Tests;

public class PaymentsTestFactory : BaseTestFactory<PaymentsContext>
{
    protected override IServiceCollection ReplaceNeededDbContexts(IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<PaymentsContext>));
        services.Remove(descriptor!);
        services.AddDbContext<PaymentsContext>(SetupTestContext());

        descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<PaymentsContext>));
        services.Remove(descriptor!);
        services.AddDbContext<PaymentsContext>(SetupTestContext());

        var descriptorTours = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ToursContext>));
        services.Remove(descriptorTours!);
        services.AddDbContext<ToursContext>(SetupTestContext());

        return services;
    }
}
