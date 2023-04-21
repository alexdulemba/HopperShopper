using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopperShopper.Entities
{
  public class PaymentMethodType
  {
    [Required]
    public int Id { get; set; }

    [Required]
    public string Type { get; set; }

    public string Description { get; set; }

    [ForeignKey("ObjectID")]
    public Guid PaymentMethodObjectID { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
  }
}
