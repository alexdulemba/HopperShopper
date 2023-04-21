using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopperShopper.Entities
{
  public class SearchHistoryEntry
  {
    [Required]
    public int Id { get; set; }

    [Key]
    [Required]
    public Guid ObjectID { get; set; }

    [Required]
    public string Value { get; set; }

    [Required]
    public DateTime TimeEntered { get; set; }

    [ForeignKey("ObjectID")]
    public Guid CustomerObjectID { get; set; }
    public Customer Customer { get; set; }
  }
}
