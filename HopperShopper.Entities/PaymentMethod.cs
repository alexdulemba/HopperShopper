using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopperShopper.Entities
{
  public class PaymentMethod
  {
    [Required]
    public int Id { get; set; }

    [Key]
    [Required]
    public Guid ObjectID { get; set; }

    [Required]
    [DefaultValue(false)]
    public bool IsDefault { get; set; }

    [ForeignKey("ObjectID")]
    public Guid CustomerObjectID { get; set; }
    public Customer Customer { get; set; }

    public CreditCard CreditCard { get; set; }

    //public PaymentMethodType PaymentMethodType { get; set; }
    
  }
}
