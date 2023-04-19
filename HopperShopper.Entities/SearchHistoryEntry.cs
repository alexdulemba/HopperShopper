namespace HopperShopper.Entities
{
  public class SearchHistoryEntry
  {
    public int Id { get; set; }
    public Guid ObjectID { get; set; }
    public string Value { get; set; }
    public DateTime TimeEntered { get; set; }

    public Guid CustomerObjectID { get; set; }
    public Customer Customer { get; set; }
  }
}
