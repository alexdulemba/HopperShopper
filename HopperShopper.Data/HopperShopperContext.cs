using HopperShopper.Entities;
using Microsoft.EntityFrameworkCore;

namespace HopperShopper.Data
{
  public class HopperShopperContext : DbContext
  {
    public string DbPath { get; set; }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CreditCard> CreditCards { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }  
    public DbSet<PaymentMethodType> PaymentMethodsTypes { get; set; }

    public HopperShopperContext(DbContextOptions<HopperShopperContext> options) : base(options) 
    {
      var folder = Environment.SpecialFolder.LocalApplicationData;
      var path = Environment.GetFolderPath(folder);
      DbPath = Path.Join(path, "hoppershopper.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
  }
}
