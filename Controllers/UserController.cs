using MambaPointMPGS.Data;
using MambaPointMPGS.Repositories;
using MambaPointMPGS.Services;
using MambaPointMPGS.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MambaPointMPGS.Controllers;

public class UserController: Controller
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    public UserController(IUserService userService, IRoleService roleService)
    {
        _userService = userService;
        _roleService = roleService;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetAllAsync();
        return View(users);
    }

    [HttpGet]
    public IActionResult Create()
    {
        
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserViewModel model)
    {
        if (string.IsNullOrEmpty(model.FirstName) && string.IsNullOrEmpty(model.LastName))
        {
            TempData["message"] = "Error creating user";
            TempData["flag"] = "danger";
            return View(model);
        }
        else
        {
            var result = await _userService.AddUserAsync(model);
            if (result == false)
            {
                TempData["message"] = "Error creating user";
                TempData["flag"] = "danger";
                return View(model);
            }
            TempData["message"] = "User created successfully";
            TempData["flag"] = "success";
            return RedirectToAction(nameof(Index));
        }
    }
}