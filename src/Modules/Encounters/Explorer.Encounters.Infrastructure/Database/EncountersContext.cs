using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.Encounter;
using Microsoft.EntityFrameworkCore;
namespace Explorer.Encounters.Infrastructure.Database;

public class EncountersContext : DbContext
{
    public DbSet<Encounter> Encounters { get; set; }
    public DbSet<SocialEncounter> SocialEncounters { get; set; }
    public DbSet<TouristProgress> TouristProgress { get; set; }
    public DbSet<KeyPointEncounter> KeyPointEncounter { get; set; }
    public DbSet<MiscEncounter> MiscEncounters { get; set; }
    public DbSet<HiddenLocationEncounter> HiddenLocationEncounters { get; set; }


    public EncountersContext(DbContextOptions<EncountersContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("encounters");
        ConfigureEncounters(modelBuilder);
    }

    private static void ConfigureEncounters(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Encounter>().Property(item => item.Instances).HasColumnType("jsonb");
        modelBuilder.Entity<Encounter>().ToTable("Encounters").UseTptMappingStrategy();
    }
}
