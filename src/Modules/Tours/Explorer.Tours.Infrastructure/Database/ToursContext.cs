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


    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");
        
        modelBuilder.Entity<Tour>()
            .HasMany(t => t.EquipmentList)
            .WithMany(e => e.Tours)
            .UsingEntity(j => j.ToTable("TourEquipment"));

        ConfigureKeyPoint(modelBuilder);
        ConfigureReview(modelBuilder);
    }

    private static void ConfigureKeyPoint(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KeyPoint>()
            .HasOne<Tour>()
            .WithMany()
            .HasForeignKey(kp => kp.TourId);
    }

    private static void ConfigureReview(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Review>()
            .HasOne<Tour>()
            .WithMany()
            .HasForeignKey(r => r.TourId);
    }


}