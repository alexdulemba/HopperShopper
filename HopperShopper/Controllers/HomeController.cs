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
    private int _cartItemCount = 0;

    public HomeController(ILogger<HomeController> logger, HopperShopperContext context)
    {
      _logger = logger;
      _context = context;
    }

    public async Task<IActionResult> Index(List<Product>? products = null)
    {
      if (IsLoggedIn())
      {
        _cartItemCount = (await _context.Carts.FirstAsync()).ItemCount;
        ViewData["CartItemCount"] = _cartItemCount;
        return View(new ProductsModel((products is null || products.Count() == 0) ? await _context.Products.ToListAsync() : products));
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
        return Error();
      }
    }

    [HttpGet]
    [Route("/Home/Products/{productObjectID}")]
    public async Task<IActionResult> Products([FromRoute] Guid productObjectID)
    {
      ViewData["CartItemCount"] = _cartItemCount;
      try
      {
        var product = await _context.Products.SingleAsync(x => x.ObjectID.Equals(productObjectID));
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
      currentCart.ItemCount++;
      await _context.SaveChangesAsync();

      return RedirectToAction("Index"); 
    }

    [HttpPost]
    [Route("/Home/Search")]
    public IActionResult Search(string content)
    {
      try 
      {
        var searcher = SearchFactory.GetSearcher(content ?? string.Empty);
        var products = searcher.Find(_context);
        ViewData["CartItemCount"] = _cartItemCount;
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
      ViewData["CartItemCount"] = _cartItemCount;
      var customer = (await _context.Customers.ToListAsync()).First();
      return View(customer);
    }

    [HttpGet]
    public async Task<IActionResult> Cart()
    {
      ViewData["CartItemCount"] = (await _context.Carts.FirstAsync()).ItemCount;
      var cart = (await _context.Carts.ToListAsync()).First();
      return View(cart);
    }

    [HttpGet]
    public async Task<IActionResult> Orders()
    {
      ViewData["CartItemCount"] = (await _context.Carts.FirstAsync()).ItemCount;
      var orders = await _context.Orders.ToListAsync();
      return View(new OrdersModel(orders)); 
    }

    [HttpGet]
    [Route("/Home/Orders/{orderObjectID}")]
    public async Task<IActionResult> Order([FromRoute] Guid orderObjectID)
    {
      ViewData["CartItemCount"] = (await _context.Carts.FirstAsync()).ItemCount;
      var order = (await _context.Orders.ToListAsync()).Where(x => x.ObjectID.Equals(orderObjectID)).First();
      return View(new OrderModel(order.Products, order));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}