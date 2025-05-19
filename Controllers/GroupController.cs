using Microsoft.AspNetCore.Mvc;

namespace MambaPointMPGS.Controllers;

public class GroupController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}