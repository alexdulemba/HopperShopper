using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopperShopper.Entities
{
  public class Order
  {
    [Required]
    public int Id { get; set; }

    [Key]
    [Required]
    public Guid ObjectID { get; set; }

    [Required]
    public int ItemCount { get; set; }

    [Required]
    public float Total { get; set; }

    [Required]
    public DateTime DatePlaced { get; set; }

    [ForeignKey("ObjectID")] //this foreign key mapped to Customer.ObjectID?
    public Guid CustomerObjectID { get; set; }
    public Customer Customer { get; set; }

    public List<Product> Products { get; set; }
    
    //public OrderContainsProducts OrderedProducts { get; set; }
  }
}
