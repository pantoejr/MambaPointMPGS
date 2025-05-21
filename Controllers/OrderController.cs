using Microsoft.AspNetCore.Mvc;

namespace MambaPointMPGS.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
