namespace HopperShopper.Data
{
  public static class ManualDatabaseInitializer
  {
    public static async Task Initialize(HopperShopperContext context) 
    {
      throw new NotImplementedException();
      await context.SaveChangesAsync();
    }
  }
}
