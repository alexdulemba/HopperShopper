namespace HopperShopper.Entities
{
  public class Customer
  {
    public int Id { get; set; }
    public Guid ObjectID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public List<PaymentMethod> PaymentMethods { get; set; }

    public List<SearchHistoryEntry> SearchHistoryEntries { get; set; }
  }
}