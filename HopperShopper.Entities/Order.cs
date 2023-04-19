namespace HopperShopper.Entities
{
  public class Order
  {
    public int Id { get; set; }
    public Guid ObjectID { get; set; }
    public int ItemCount { get; set; }
    public float Total { get; set; }
    public DateTime DatePlaced { get; set; }
    
    public List<Product> Products { get; set; }
  }
}
