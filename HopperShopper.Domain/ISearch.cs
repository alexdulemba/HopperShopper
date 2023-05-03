using HopperShopper.Data;
using HopperShopper.Entities;

namespace HopperShopper.Domain
{
  public interface ISearch
  {
    bool Wants(string content);

    Task<List<Product>> FindAsync(HopperShopperContext content);
  }
}
