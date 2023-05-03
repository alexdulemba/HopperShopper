using HopperShopper.Data;
using HopperShopper.Entities;
using Microsoft.EntityFrameworkCore;

namespace HopperShopper.Domain
{
  public class CategorySearch : ISearch
  {
    private static string _searchAttribute = "category";
    private string Text { get; set; }

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
      return await context.Products.Include(p => p.Categories)
                             .Where(p => p.Categories.Any(c => c.Name.ToLower().Contains(Text)))
                             .ToListAsync();
    }
  }
}
