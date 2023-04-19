namespace HopperShopper.Entities
{
  public class PaymentMethod
  {
    public int Id { get; set; }
    public Guid ObjectID { get; set; }


    public Guid CustomerObjectID { get; set; }
    public Customer Customer { get; set; }
    
  }
}
