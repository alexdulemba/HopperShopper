namespace HopperShopper.Entities
{
  public class CreditCard
  {
    public int Id { get; set; }
    public Guid ObjectID { get; set; }
    public string CardholderName { get; set; }
    public string AccountNumber { get; set; }
    public string CCV { get; set; }
    public DateTime ExpirationDate { get; set; }

    public PaymentMethodType PaymentMethodType { get; set; }

    public Guid PaymentMethodObjectId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
  }
}
