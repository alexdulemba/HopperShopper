using HopperShopper.Data;
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

    public HomeController(ILogger<HomeController> logger, HopperShopperContext context)
    {
      _logger = logger;
      _context = context;
    }

    public async Task<IActionResult> Index()
    {
      ViewData["CartItemCount"] = (await _context.Carts.FirstAsync()).ItemCount;
      return View(new ProductsModel(await _context.Products.ToListAsync()));
    }

    [HttpGet]
    [Route("/Home/Products/{productObjectID}")]
    public async Task<IActionResult> Products([FromRoute] Guid productObjectID)
    {
      ViewData["CartItemCount"] = (await _context.Carts.FirstAsync()).ItemCount;
      var product = (await _context.Products.ToListAsync()).Where(x => x.ObjectID.Equals(productObjectID)).First();
      return View(product);
    }

    [HttpPost]
    [Route("/Home/AddProductToCart")]
    public async Task<IActionResult> AddProductToCart([FromForm] Product product)
    {
      await _context.AddAsync(product);
      await _context.SaveChangesAsync();

      (await _context.Carts.FirstAsync()).ItemCount++;
      await _context.SaveChangesAsync();

      return RedirectToAction("Index"); 
    }

    [HttpGet]
    [Route("/Home/Customers/")]
    public async Task<IActionResult> Customers()
    {
      ViewData["CartItemCount"] = (await _context.Carts.FirstAsync()).ItemCount;
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