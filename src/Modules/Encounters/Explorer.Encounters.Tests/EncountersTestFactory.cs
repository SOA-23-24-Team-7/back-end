using Explorer.BuildingBlocks.Tests;
using Explorer.Encounters.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Encounters.Tests;

public class EncountersTestFactory : BaseTestFactory<EncountersContext>
{
    protected override IServiceCollection ReplaceNeededDbContexts(IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<EncountersContext>));
        services.Remove(descriptor!);
        services.AddDbContext<EncountersContext>(SetupTestContext());

        return services;
    }
}
