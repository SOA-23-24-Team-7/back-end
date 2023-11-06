using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database;

public class StakeholdersContext : DbContext
{

    public DbSet<User> Users { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Follower> Followers { get; set; }
    public DbSet<ClubInvitation> ClubInvitations { get; set; }
    public DbSet<ClubMembership> ClubMemberships { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<ClubJoinRequest> ClubJoinRequests { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Problem> Problem { get; set; }
    public DbSet<ProblemAnswer> ProblemAnswer { get; set; }
    public DbSet<ProblemComment> ProblemComment { get; set; }
    public DbSet<Message> Messages { get; set; }

    public StakeholdersContext(DbContextOptions<StakeholdersContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("stakeholders");

        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

        modelBuilder.Entity<ClubJoinRequest>().HasKey(r => r.Id);

        ConfigureStakeholder(modelBuilder);
    }

    private static void ConfigureStakeholder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasOne(r => r.User)
            .WithOne()
            .HasForeignKey<Person>(s => s.UserId);
        modelBuilder.Entity<ClubJoinRequest>()
            .HasOne(r => r.Tourist)
            .WithMany()
            .HasForeignKey(r => r.TouristId);

        modelBuilder.Entity<ClubJoinRequest>()
            .HasOne(r => r.Club)
            .WithMany()
            .HasForeignKey(r => r.ClubId);

        modelBuilder.Entity<Club>()
            .HasOne(c => c.Owner)
            .WithMany()
            .HasForeignKey(c => c.OwnerId);

        modelBuilder.Entity<ClubInvitation>()
            .HasOne(i => i.Club)
            .WithMany()
            .HasForeignKey(i => i.ClubId);

        modelBuilder.Entity<Rating>()
           .HasOne(s => s.User)
           .WithOne()
           .HasForeignKey<Rating>(s => s.UserId);

        modelBuilder.Entity<Follower>()
                    .HasOne(m => m.User)
                    .WithMany(m => m.Followers)
                    .HasForeignKey(k => k.UserId);

        modelBuilder.Entity<Follower>()
            .HasOne(m => m.FollowedBy)
            .WithMany(m => m.Following)
            .HasForeignKey(k => k.FollowedById);

    }
}