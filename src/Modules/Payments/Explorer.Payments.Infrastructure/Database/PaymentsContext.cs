using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.Bundles;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database;

public class PaymentsContext : DbContext
{

    public DbSet<TourToken> tourTokens { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<BundleOrderItem> BundleOrderItems { get; set; }

    public DbSet<Wallet> Wallets { get; set; }

    public DbSet<Record> Records { get; set; }
    public DbSet<TransactionRecord> TransactionRecords { get; set; }

    public DbSet<ShoppingNotification> ShoppingNotifications { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<Bundle> Bundles { get; set; }
    public DbSet<BundleItem> BundleItems { get; set; }

    public PaymentsContext(DbContextOptions<PaymentsContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("payments");

        ConfigurePayments(modelBuilder);
        ConfigureBundles(modelBuilder);
    }

    private static void ConfigurePayments(ModelBuilder modelBuilder)
    {
    }

    private static void ConfigureBundles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bundle>()
             .HasMany(b => b.BundleItems)
             .WithOne()
             .HasForeignKey(bi => bi.BundleId)
             .IsRequired();
    }
}
