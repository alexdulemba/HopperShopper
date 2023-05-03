using Bogus;
using HopperShopper.Entities;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace HopperShopper.Data
{
  public static class BogusDatabaseInitializer
  {
    public static async Task InitializeAsync(HopperShopperContext context, bool deleteAndRegenerate)
    {
      if (deleteAndRegenerate) {
        context.Customers.RemoveRange(context.Customers);
        context.PaymentMethods.RemoveRange(context.PaymentMethods);
        context.CreditCards.RemoveRange(context.CreditCards);
        context.CustomerSearchHistory.RemoveRange(context.CustomerSearchHistory);
        context.ProductCategories.RemoveRange(context.ProductCategories);
        context.Products.RemoveRange(context.Products);
        context.Carts.RemoveRange(context.Carts);
        context.Orders.RemoveRange(context.Orders);
        await context.SaveChangesAsync();

        Debug.WriteLine("Removed all previous DB table entries");
      }

      //var paymentMethodTypes = CreatePaymentMethodTypes();
      //await context.AddRangeAsync(paymentMethodTypes);

      var customers = CreateCustomers();
      await context.AddRangeAsync(customers);
      //await context.SaveChangesAsync();

      var paymentMethods = CreatePaymentMethods(customers.First());
      await context.AddRangeAsync(paymentMethods);
      //await context.SaveChangesAsync();

      var creditCards = CreateCreditCards(paymentMethods, customers.First().FirstName);
      await context.AddRangeAsync(creditCards);
      //await context.SaveChangesAsync();

      var searchHistory = CreateSearchHistory(customers.First());
      await context.AddRangeAsync(searchHistory);
      //await context.SaveChangesAsync();

      var productCategories = CreateProductCategories();
      await context.AddRangeAsync(productCategories);
      //await context.SaveChangesAsync();

      var products = CreateProducts(productCategories);
      await context.AddRangeAsync(products);
      //await context.SaveChangesAsync();

      var cart = CreateCart(customers.First(), products);
      await context.AddRangeAsync(cart);
      //await context.SaveChangesAsync();

      var orders = CreateOrders(customers.First(), products);
      await context.AddRangeAsync(orders);

      await context.SaveChangesAsync();
    }

    private static List<Customer> CreateCustomers()
    {
      //only one customer for now, times are tough...
      return new List<Customer>()
      {
        new Customer()
        {
          Id = 1,
          ObjectID = Guid.NewGuid(),
          FirstName = "Kevin",
          LastName = "Mitnick",
          Username = "kmitnick132",
          Password = "superSecurePassw0rd",
        }
      };
    }

    private static List<PaymentMethod> CreatePaymentMethods(Customer customer)
    {
      return new List<PaymentMethod>()
      {
        new PaymentMethod()
        {
          Id = 1,
          ObjectID = new Guid(),
          IsDefault = true,
          CustomerObjectID = customer.ObjectID
        },
        new PaymentMethod() 
        {
          Id = 2,
          ObjectID = new Guid(),
          IsDefault = false,
          CustomerObjectID = customer.ObjectID
        }
      };
    }

    private static List<CreditCard> CreateCreditCards(List<PaymentMethod> paymentMethods, string name = "")
    {
      var faker = new Faker<CreditCard>()
          .RuleFor(x => x.Id, f => f.IndexFaker)
          .RuleFor(x => x.ObjectID, new Guid())
          .RuleFor(x => x.CardholderName, name)
          .RuleFor(x => x.AccountNumber, f => f.Finance.CreditCardNumber())
          .RuleFor(x => x.CCV, f => f.Finance.CreditCardCvv())
          .RuleFor(x => x.ExpirationDate, f => f.Date.Soon(60));

      var cards = faker.Generate(2);
      cards[0].PaymentMethodObjectID = paymentMethods[0].ObjectID;
      cards[1].PaymentMethodObjectID = paymentMethods[1].ObjectID;

      return cards;
    }

    private static List<SearchHistoryEntry> CreateSearchHistory(Customer customer)
    {
      var faker = new Faker<SearchHistoryEntry>()
        .RuleFor(x => x.Id, f => f.IndexFaker)
        .RuleFor(x => x.ObjectID, new Guid())
        .RuleFor(x => x.Value, f => f.Random.Words(4))
        .RuleFor(x => x.TimeEntered, f => f.Date.Past(60))
        .RuleFor(x => x.CustomerObjectID, customer.ObjectID);

      return faker.Generate(25);
    }

    private static List<ProductCategory> CreateProductCategories()
    {
      var faker = new Faker<ProductCategory>()
          .RuleFor(x => x.Id, f => f.IndexFaker)
          .RuleFor(x => x.ObjectID, new Guid())
          .RuleFor(x => x.Name, f => f.Commerce.Categories(5).FirstOrDefault())
          .RuleFor(x => x.Description, string.Empty);

      return faker.Generate(10);
    }

    private static List<Product> CreateProducts(List<ProductCategory> categories)
    {
      var random = new Random();
      var faker = new Faker<Product>()
        .RuleFor(x => x.Id, f => f.IndexFaker)
        .RuleFor(x => x.ObjectID, new Guid())
        .RuleFor(x => x.Name, f => f.Commerce.ProductName())
        .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
        .RuleFor(x => x.Price, f => (float)f.Finance.Amount(min: 5, max: 200))
        .RuleFor(x => x.Categories, categories.Where(x => x.Id < random.Next(20)).ToList());
      
      return faker.Generate(20);
    }

    private static List<Cart> CreateCart(Customer customer, List<Product> products)
    {
      var random = new Random();
      var cartProducts = products.Where(x => x.Id < random.Next(20));
      Debug.WriteLine($"Cart Product Count: {cartProducts.Count()}");
      Debug.WriteLine($"Cart Subtotal: ${cartProducts.Sum(p => p.Price)}");

      return new List<Cart>()
      {
        new Cart()
        {
          Id = 1,
          ObjectID = new Guid(),
          Subtotal = cartProducts.Sum(x => x.Price),
          CustomerObjectID = customer.ObjectID,
          Products = cartProducts.ToList()
        }
      };
    }

    private static List<Order> CreateOrders(Customer customer, List<Product> products)
    {
      var random = new Random();
      var orderProducts = products.Where(x => x.Id < random.Next(20));

      return new List<Order>()
      {
        new Order()
        {
          Id = 1,
          ObjectID = new Guid(),
          ItemCount = orderProducts.Count(),
          Total = orderProducts.Sum(x => x.Price),
          CustomerObjectID = customer.ObjectID,
          DatePlaced = DateTime.UtcNow,
          Products = orderProducts.ToList()
        }
      };
    }

    [Obsolete]
    private static List<PaymentMethodType> CreatePaymentMethodTypes()
    {
      return new List<PaymentMethodType>()
      {
        new PaymentMethodType()
        {
          Id = 1,
          Type = "Credit Card",
          Description = "Usable line of credit from your bank"
        }
      };
    }

  }
}
