using Microsoft.AspNetCore.Mvc;

namespace UwpAspNetLib.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}