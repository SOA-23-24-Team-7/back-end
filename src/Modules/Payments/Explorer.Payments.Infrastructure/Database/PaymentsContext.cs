using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database;

public class PaymentsContext : DbContext
{

    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<TourToken> tourTokens { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }

    public DbSet<Wallet> Wallets { get; set; }

    public DbSet<Record> Records { get; set; }

    public DbSet<ShoppingNotification> ShoppingNotifications { get; set; }
    public DbSet<Coupon> Coupons { get; set; }

    public PaymentsContext(DbContextOptions<PaymentsContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("payments");

        ConfigurePayments(modelBuilder);
    }

    private static void ConfigurePayments(ModelBuilder modelBuilder)
    {
       
    }

    
}
