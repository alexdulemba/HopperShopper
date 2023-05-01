using System.ComponentModel.DataAnnotations;

namespace HopperShopper.Entities
{
  public class Customer
  {
    [Required]
    public int Id { get; set; }

    [Key]
    [Required]
    public Guid ObjectID { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; }

    [Required]
    [MaxLength(50)]
    public string Password { get; set; }
    
    public List<Order> Orders { get; set; }

    public Cart Cart { get; set; }

    public List<PaymentMethod> PaymentMethods { get; set; }

    public List<SearchHistoryEntry> SearchHistoryEntries { get; set; }
  }
}