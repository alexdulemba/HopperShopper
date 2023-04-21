using HopperShopper.Entities;

namespace HopperShopper.Web.Models
{
  public class OrdersModel
  {
    public List<Order> Orders { get; set; }

    public OrdersModel(List<Order> orders)
    {
      Orders = orders;
    }
  }
}
