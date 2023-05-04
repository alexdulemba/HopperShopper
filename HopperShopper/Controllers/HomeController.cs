using HopperShopper.Data;
using HopperShopper.Domain;
using HopperShopper.Entities;
using HopperShopper.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HopperShopper.Web.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly HopperShopperContext _context;
    private readonly string _sessionKey = "LoginSessionKey";
    private readonly string _sessionValue = "LoginSessionValue1234";
    private readonly string _sessionCartItemCount = "SessionCartItemCount";

    public HomeController(ILogger<HomeController> logger, HopperShopperContext context)
    {
      _logger = logger;
      _context = context;
    }

    public async Task<IActionResult> Index(List<Product>? products = null)
    {
      if (IsLoggedIn())
      {
        SetCartItemCount((await _context.Carts.Include(c => c.Products).FirstAsync()).Products.Count());
        ViewData["CartItemCount"] = GetCartItemCount();
        return View(new ProductsModel((products is null || products.Count() == 0) 
                                      ? await _context.Products.Include(p => p.Categories).ToListAsync() 
                                      : products));
      }
      else 
      {
        return RedirectToAction("Login");
      }
    }

    private bool IsLoggedIn()
    {
      var session = HttpContext.Session.GetString(_sessionKey);
      return !string.IsNullOrEmpty(session);
    }

    [HttpGet]
    public IActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] LoginModel loginInformation)
    {
      var userExists = await _context.Customers.AnyAsync(c => c.Username == loginInformation.Username
                                                        && c.Password == loginInformation.Password);
      
      if (userExists)
      {
        HttpContext.Session.SetString(_sessionKey, _sessionValue);
        return RedirectToAction("Index");
      }
      else
      {
        return Login();
      }
    }

    [HttpGet]
    [Route("/Home/Products/{productObjectID}")]
    public async Task<IActionResult> Products([FromRoute] Guid productObjectID)
    {
      ViewData["CartItemCount"] = GetCartItemCount();
      try
      {
        var product = await _context.Products.Include(p => p.Categories).SingleAsync(x => x.ObjectID.Equals(productObjectID));
        return View(product);
      }
      catch (Exception) 
      {
        return Error();
      }
    }

    [HttpPost]
    [Route("/Home/AddProductToCart")]
    public async Task<IActionResult> AddProductToCart([FromForm] Product product)
    {
      var currentCart = await _context.Carts.Include(x => x.Products).FirstAsync();
      var currentProduct = await _context.Products.SingleAsync(x => x.ObjectID.Equals(product.ObjectID));

      currentCart.Products.Add(currentProduct);
      await _context.SaveChangesAsync();

      SetCartItemCount(GetCartItemCount() + 1);
      return RedirectToAction("Index"); 
    }

    [HttpPost]
    [Route("/Home/RemoveProductFromCart")]
    public async Task<IActionResult> RemoveProductFromCart([FromForm] Guid objectID)
    {
      var currentCart = await _context.Carts.Include(x => x.Products).FirstAsync();
      var currentProduct = await _context.Products.SingleAsync(x => x.ObjectID.Equals(objectID));

      currentCart.Products.Remove(currentProduct);
      await _context.SaveChangesAsync();

      SetCartItemCount(GetCartItemCount() - 1);
      return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("/Home/Search")]
    public async Task<IActionResult> Search(string content)
    {
      try 
      {
        var searcher = SearchFactory.GetSearcher(content ?? string.Empty);
        var products = await searcher.FindAsync(_context);
        ViewData["CartItemCount"] = GetCartItemCount();
        return View("Index", new ProductsModel(products));
      } 
      catch (Exception ex)
      {
        Debug.WriteLine(ex);
        return Error();
      }
    }

    [HttpGet]
    [Route("/Home/Customers/")]
    public async Task<IActionResult> Customers()
    {
      ViewData["CartItemCount"] = GetCartItemCount();
      var customer = (await _context.Customers.ToListAsync()).First();
      return View(customer);
    }

    [HttpPost]
    [Route("/Home/Customers/")]
    public async Task<IActionResult> Customers(Customer customer)
    {
      ViewData["CartItemCount"] = GetCartItemCount();
      var databaseCustomer = await _context.Customers.SingleAsync(c => c.ObjectID.Equals(customer.ObjectID));

      databaseCustomer.UpdateFrom(customer);
      await _context.SaveChangesAsync();

      return View(customer);
    }

    [HttpGet]
    public async Task<IActionResult> Cart()
    {
      ViewData["CartItemCount"] = GetCartItemCount();
      var cart = (await _context.Carts.Include(c => c.Products).ToListAsync()).First();
      return View(cart);
    }

    [HttpPost]
    [Route("/Home/Checkout")]
    public async Task<IActionResult> Checkout([FromForm] Guid objectId)
    {
      var cart = await _context.Carts.Include(c => c.Products).Include(c => c.Customer).SingleAsync(c => c.ObjectID.Equals(objectId));
      var order = new Order()
      {
        Id = await _context.Orders.CountAsync() + 1,
        ObjectID = Guid.NewGuid(),
        Products = new List<Product>(cart.Products),
        ItemCount = cart.Products.Count(),
        Total = cart.Products.Sum(p => p.Price),
        DatePlaced = DateTime.UtcNow,
        Customer = cart.Customer,
        CustomerObjectID = cart.CustomerObjectID,
      };

      await _context.Orders.AddAsync(order);
      cart.Products.RemoveAll((p) => true);
      await _context.SaveChangesAsync();

      return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Orders()
    {
      ViewData["CartItemCount"] = GetCartItemCount();
      var orders = await _context.Orders.ToListAsync();
      return View(new OrdersModel(orders)); 
    }

    [HttpGet]
    [Route("/Home/Orders/{orderObjectID}")]
    public async Task<IActionResult> Order([FromRoute] Guid orderObjectID)
    {
      ViewData["CartItemCount"] = GetCartItemCount();
      var order = (await _context.Orders.Include(o => o.Products).ToListAsync()).Single(x => x.ObjectID.Equals(orderObjectID));
      return View(new OrderModel(order.Products, order));
    }

    private int GetCartItemCount()
    {
      return HttpContext.Session.GetInt32(_sessionCartItemCount) ?? -1;
    }

    private void SetCartItemCount(int value)
    {
      HttpContext.Session.SetInt32(_sessionCartItemCount, value);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}