using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<KeyPoint> KeyPoints { get; set; }
    public DbSet<Facility> Facilities { get; set; }
    public DbSet<Preference> Preferences { get; set; }

    public DbSet<PublicKeyPointRequest> PublicKeyPointRequests { get; set; }

    public DbSet<TouristEquipment> TouristEquipments { get; set; }
    public DbSet<PublicFacilityRequest> PublicFacilityRequests { get; set; }
    public DbSet<PublicKeyPointNotification> PublicKeyPointNotifications { get; set; }
    public DbSet<PublicFacilityNotification> PublicFacilityNotifications { get; set; }
    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");
        
        modelBuilder.Entity<Tour>()
            .HasMany(t => t.EquipmentList)
            .WithMany(e => e.Tours)
            .UsingEntity(j => j.ToTable("TourEquipment"));

        ConfigureKeyPoint(modelBuilder);
        ConfigurePublicKeyPointRequest(modelBuilder);
        ConfigurePublicFacilityRequest(modelBuilder);
    }

    private static void ConfigureKeyPoint(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tour>()
            .HasMany(t => t.KeyPoints)
            .WithOne(k => k.Tour);

        modelBuilder.Entity<KeyPoint>()
            .HasOne<Tour>()
            .WithMany(t => t.KeyPoints)
            .HasForeignKey(kp => kp.TourId);
    }

    private static void ConfigurePublicKeyPointRequest(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PublicKeyPointRequest>()
            .HasOne<KeyPoint>()
            .WithOne()
            .HasForeignKey<PublicKeyPointRequest>(s => s.KeyPointId);
    }

    private static void ConfigurePublicFacilityRequest(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PublicFacilityRequest>()
            .HasOne<Facility>()
            .WithOne()
            .HasForeignKey<PublicFacilityRequest>(s => s.FacilityId);
    }
}