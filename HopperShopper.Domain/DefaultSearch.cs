using HopperShopper.Data;
using HopperShopper.Entities;

namespace HopperShopper.Domain
{
  public class DefaultSearch : ISearch
  {
    private static string _searchAttribute = "name";
    private string Text { get; set; } = string.Empty;

    public bool Wants(string content)
    {
      if (content.ToLower().Contains(_searchAttribute)) 
      { 
        Text = content.ToLower().Substring(_searchAttribute.Length).Trim();      
        return true;
      }

      return false;
    }

    public List<Product> Find(HopperShopperContext context)
    {
      if (Text != string.Empty) 
      {
        return context.Products.Where(p => p.Name.ToLower().Contains(Text)).ToList();
      }

      return context.Products.ToList();
    }
  }
}
