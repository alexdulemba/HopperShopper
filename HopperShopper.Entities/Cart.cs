namespace HopperShopper.Entities
{
  public class Cart
  {
    public int Id { get; set; }
    public Guid ObjectID { get; set; }
    public int ItemCount { get; set; }
    public float Subtotal { get; set; }

    public Guid CustomerObjectID { get; set; }
    public Customer Customer { get; set; }

    public List<Product> Products { get; set; }
  }
}
