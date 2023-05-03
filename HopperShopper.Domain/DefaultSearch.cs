using HopperShopper.Data;
using HopperShopper.Entities;
using Microsoft.EntityFrameworkCore;

namespace HopperShopper.Domain
{
  public class DefaultSearch : ISearch
  {
    private static string _searchAttribute = "name";
    private string Text { get; set; } = string.Empty;

    public DefaultSearch() { }  

    public DefaultSearch(string content) 
    {
      Text = content.ToLower().Trim();
    }

    public bool Wants(string content)
    {
      if (content.ToLower().Contains(_searchAttribute)) 
      { 
        Text = content.ToLower().Substring(_searchAttribute.Length).Trim();      
        return true;
      }

      return false;
    }

    public async Task<List<Product>> FindAsync(HopperShopperContext context)
    {
      if (Text != string.Empty) 
      {
        return await context.Products.Where(p => p.Name.ToLower().Contains(Text)).ToListAsync();
      }

      return await context.Products.ToListAsync();
    }
  }
}
