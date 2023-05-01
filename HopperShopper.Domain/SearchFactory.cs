using HopperShopper.Entities;

namespace HopperShopper.Domain
{
  public static class SearchFactory
  {
    private static ISearch[] Searchers = new ISearch[]
    {
      new DefaultSearch(),
      new CategorySearch(),
    };

    public static ISearch GetSearcher(string content)
    {
      foreach (var searcher in Searchers) 
      {
        if (searcher.Wants(content))
        {
          return searcher;
        }
      };

      return new DefaultSearch();
    }
  }
}
