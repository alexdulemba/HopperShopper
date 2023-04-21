using System.ComponentModel.DataAnnotations;

namespace HopperShopper.Entities
{
  public class Product
  {
    [Required]
    public int Id { get; set; }

    [Key]
    [Required]
    public Guid ObjectID { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public float Price { get; set; }

    public List<ProductCategory> Categories { get; set; }

    public List<Order> Orders { get; set; }

    public List<Cart> Carts { get; set; }
    //public List<OrderContainsProducts> MappedOrders { get; set; }
  }
}
