using HopperShopper.Entities;

namespace HopperShopper.Data
{
  public static class DatabaseInitializer
  {
    public static async Task InitializeAsync(HopperShopperContext context)
    {
      if (context.Products.Any()) return;

      var customers = CreateCustomers();
      await context.AddRangeAsync(customers);

      var products = CreateProducts();
      await context.AddRangeAsync(products);

      var productCategories = CreateProductCategories();
      await context.AddRangeAsync(productCategories);

      var creditCards = CreateCreditCards();
      await context.AddRangeAsync(creditCards);

      var paymentMethods = CreatePaymentMethods();
      await context.AddRangeAsync(paymentMethods);

      var paymentMethodTypes =  CreatePaymentMethodTypes();
      await context.AddRangeAsync(paymentMethodTypes);

      await context.SaveChangesAsync();
    }

    private static Customer[] CreateCustomers()
    {
      throw new NotImplementedException();
    }

    private static Product[] CreateProducts()
    {
      throw new NotImplementedException();
    }

    private static ProductCategory[] CreateProductCategories()
    {
      throw new NotImplementedException();
    }

    private static CreditCard[] CreateCreditCards()
    {
      throw new NotImplementedException();
    }

    private static PaymentMethod[] CreatePaymentMethods()
    {
      throw new NotImplementedException();
    }

    private static PaymentMethodType[] CreatePaymentMethodTypes()
    {
      throw new NotImplementedException();
    }

  }
}
