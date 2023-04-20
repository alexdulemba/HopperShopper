using HopperShopper.Entities;

namespace HopperShopper.Web.Models
{
  public class ProductsModel
  {
    public List<Product> Products { get; set; }

    public ProductsModel() 
    {
      //get products entities from DB
      Products = new()
      {
        new Product { Id = 1, Name = "Sample Product 1" },
        new Product { Id = 2, Name = "Sample Product 2" },
        new Product { Id = 3, Name = "Sample Product 3" },
        new Product { Id = 4, Name = "Sample Product 4" },
      };
    }
  }
}
