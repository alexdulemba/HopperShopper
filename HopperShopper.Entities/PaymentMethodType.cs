namespace HopperShopper.Entities
{
  public class PaymentMethodType
  {
    public string Type { get; set; }
    public string Description { get; set; }

    public Guid PaymentMethodObjectID { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
  }
}
