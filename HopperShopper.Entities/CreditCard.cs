using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopperShopper.Entities
{
  public class CreditCard
  {
    [Required]
    public int Id { get; set; }

    [Key]
    [Required]
    public Guid ObjectID { get; set; }

    [Required]
    public string CardholderName { get; set; }

    [Required]
    public string AccountNumber { get; set; }

    [Required]
    public string CCV { get; set; }

    [Required]
    public DateTime ExpirationDate { get; set; }

    [ForeignKey("ObjectID")]
    public Guid PaymentMethodObjectID { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
  }
}
