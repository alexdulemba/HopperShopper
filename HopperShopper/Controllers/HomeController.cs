using HopperShopper.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HopperShopper.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    public IActionResult Index()
    {
      return View();
    }

    [HttpGet]
    [Route("/Products/{productObjectID}")]
    public IActionResult Products([FromRoute] Guid productObjectID)
    {
      return View();
    }

    [HttpGet]
    [Route("/Customers/{customerObjectID}")]
    public IActionResult Customers([FromRoute] Guid customerObjectID)
    {
      return View();
    }

    public IActionResult Cart()
    {
      return View();
    }

    public IActionResult Orders()
    { 
      return View(); 
    }

    [HttpGet]
    [Route("/Orders/{orderObjectID}")]
    public IActionResult Order([FromRoute] Guid orderObjectID)
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}