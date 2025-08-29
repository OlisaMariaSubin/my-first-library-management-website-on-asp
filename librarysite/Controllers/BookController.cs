using Microsoft.AspNetCore.Mvc;

namespace librarysite.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
