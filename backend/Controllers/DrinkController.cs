using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    public class DrinkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
