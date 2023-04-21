using HopperShopper.Entities;

namespace HopperShopper.Web.Models
{
  public class OrderModel : ProductsModel
  {
    public Order Order { get; set; }

    public OrderModel(List<Product> products, Order order) : base(products)
    {
      Order = order;
    }
  }
}
