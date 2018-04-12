using Microsoft.AspNetCore.Mvc;

namespace UwpControllerLib
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}