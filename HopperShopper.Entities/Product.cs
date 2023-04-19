namespace HopperShopper.Entities
{
  public class Product
  {
    public int Id { get; set; }
    public Guid ObjectID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }

    public List<ProductCategory> Categories { get; set; }
  }
}
