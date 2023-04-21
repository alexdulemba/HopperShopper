using HopperShopper.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HopperShopper.Data
{
  public class HopperShopperContext : DbContext
  {
    public string? DbPath { get; set; }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<SearchHistoryEntry> CustomerSearchHistory { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<CreditCard> CreditCards { get; set; }
    //public DbSet<PaymentMethodType> PaymentMethodsTypes { get; set; }

    public HopperShopperContext(DbContextOptions<HopperShopperContext> options) : base(options)
    {
      //DbPath = options.ContextType.Assembly.Location;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // explicit convention discovery
      #region Customers Table's relations
      modelBuilder.Entity<Customer>()
        .HasMany(e => e.Orders)   // customer has many orders
        .WithOne(e => e.Customer) // many orders belong to one customer
        .HasForeignKey(e => e.CustomerObjectID) // FK constraint
        .IsRequired();

      modelBuilder.Entity<Customer>()
        .HasOne(e => e.Cart)
        .WithOne(e => e.Customer)
        .HasForeignKey(typeof(Cart), "CustomerObjectID")
        .IsRequired();

      modelBuilder.Entity<Customer>()
        .HasMany(e => e.SearchHistoryEntries)
        .WithOne(e => e.Customer)
        .HasForeignKey(e => e.CustomerObjectID)
        .IsRequired();

      modelBuilder.Entity<Customer>()
        .HasMany(e => e.PaymentMethods)
        .WithOne(e => e.Customer)
        .HasForeignKey(e => e.CustomerObjectID)
        .IsRequired();
      #endregion

      #region PaymentMethod Table's relations
      modelBuilder.Entity<PaymentMethod>()
        .HasOne(e => e.CreditCard)
        .WithOne(e => e.PaymentMethod)
        .HasForeignKey(typeof(CreditCard), "PaymentMethodObjectID")
        .IsRequired();

      //modelBuilder.Entity<PaymentMethod>()
      //  .HasOne(e => e.PaymentMethodType)
      //  .WithOne(e => e.PaymentMethod)
      //  .HasForeignKey(typeof(PaymentMethodType), "PaymentMethodObjectID")
      //  .IsRequired();
      #endregion

      modelBuilder.Entity<Order>()
        .HasMany(e => e.Products)
        .WithMany(e => e.Orders);

      modelBuilder.Entity<Product>()
        .HasMany(e => e.Categories)
        .WithMany(e => e.Products);

      modelBuilder.Entity<Cart>()
        .HasMany(e => e.Products)
        .WithMany(e => e.Carts);

      base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);
      optionsBuilder.EnableSensitiveDataLogging(true);
      //optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
  }
}
