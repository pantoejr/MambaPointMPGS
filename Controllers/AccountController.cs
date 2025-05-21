using MambaPointMPGS.Data;
using MambaPointMPGS.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MambaPointMPGS.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    // GET
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            SetTempDataMessage("Invalid input. Please try again.", "danger");
            return View(model);
        }
        try
        {
            var user = await _userManager.FindByEmailAsync(model.UserName);
            if (user == null || !user.IsActive)
            {
                SetTempDataMessage("Incorrect Username and Password", "danger");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                SetTempDataMessage("Login successfull", "success");
                return RedirectToAction("Index", "Home");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            SetTempDataMessage("An error occurred. Please try again later.", "danger");
        }

        SetTempDataMessage("Incorrect Username and Password", "danger");
        return View(model);
    }

    private void SetTempDataMessage(string message, string flag)
    {
        TempData["Message"] = message;
        TempData["Flag"] = flag;
    }


    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }

    public async Task<IActionResult> ChangePassword()
    {
        var user = await _userManager.GetUserAsync(User);
        return View(user);
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