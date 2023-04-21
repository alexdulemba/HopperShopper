using System.ComponentModel.DataAnnotations;

namespace HopperShopper.Entities
{
  public class OrderContainsProducts
  {
    [Required]
    public int Id { get; set; }

    //public Guid OrderObjectID { get; set; }
    //public Order Order { get; set; }

    public List<Order> Orders { get; set; }

    public List<Product> Products { get; set; }

  }
}
