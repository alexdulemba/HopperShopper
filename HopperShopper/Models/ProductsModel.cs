using HopperShopper.Entities;

namespace HopperShopper.Web.Models
{
  public class ProductsModel
  {
    public List<Product> Products { get; set; }

    public ProductsModel(List<Product> products) 
    {
      Products = products;
    }
  }
}
