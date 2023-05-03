using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopperShopper.Entities
{
  public class Cart
  {
    [Required]
    public int Id { get; set; }

    [Key]
    [Required]
    public Guid ObjectID { get; set; }

    [Required]
    public float Subtotal { get; set; }

    [ForeignKey("ObjectID")]
    public Guid CustomerObjectID { get; set; }
    public Customer Customer { get; set; }

    public List<Product> Products { get; set; }
  }
}
