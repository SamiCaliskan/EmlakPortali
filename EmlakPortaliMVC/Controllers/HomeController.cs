using Microsoft.AspNetCore.Mvc;

namespace EmlakPortaliMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Advert()
        {
            return View();
        }
        public IActionResult ForRent()
        {
            return View();
        }
        public IActionResult ForSale()
        {
            return View();
        }
    }
}