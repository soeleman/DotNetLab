using Microsoft.AspNetCore.Mvc;

namespace Soeleman.Mvc.Controllers
{
    public class HomeController :
        Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}