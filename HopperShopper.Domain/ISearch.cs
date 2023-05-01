using HopperShopper.Data;
using HopperShopper.Entities;

namespace HopperShopper.Domain
{
  public interface ISearch
  {
    bool Wants(string content);

    List<Product> Find(HopperShopperContext content);
  }
}
