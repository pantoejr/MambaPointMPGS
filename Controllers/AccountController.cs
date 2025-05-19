using MambaPointMPGS.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MambaPointMPGS.Controllers;

public class AccountController : Controller
{
    // GET
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model)
    {
        if (String.IsNullOrEmpty(model.UserName) && String.IsNullOrEmpty(model.Password))
        {
            TempData["message"] = "Please enter your username and password";
            TempData["flag"] = "danger";
            return RedirectToAction("Login");
        }
        else
        {
            
            return View();
        }
        
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ForgotPassword(ForgotPasswordViewModel model)
    {
        if (String.IsNullOrEmpty(model.Email))
        {
            TempData["message"] = "Please enter your email";
            TempData["flag"] = "danger";
            return RedirectToAction("ForgotPassword");
        }
        else
        {
            return View();
        }
    }
}