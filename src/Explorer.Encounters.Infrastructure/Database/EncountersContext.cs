using Microsoft.EntityFrameworkCore;

namespace Explorer.Encounters.Infrastructure.Database;

public class EncountersContext : DbContext
{
    //definitions for dbSet
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("encounters");
        ConfigureEncounters(modelBuilder);
    }

    private static void ConfigureEncounters(ModelBuilder modelBuilder)
    {

    }
}
