using MambaPointMPGS.Data;
using MambaPointMPGS.Repositories;
using MambaPointMPGS.Services;
using MambaPointMPGS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MambaPointMPGS.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly AppDbContext _context;
    public UserController(IUserService userService, IRoleService roleService, AppDbContext context)
    {
        _userService = userService;
        _roleService = roleService;
        _context = context;
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
        ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name");
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
                ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", model.GroupId);
                return View(model);
            }
            TempData["message"] = "User created successfully";
            TempData["flag"] = "success";
            return RedirectToAction(nameof(Index));
        }
    }

    public async Task<IActionResult> Edit(string Id)
    {
        var existingUser = await _userService.GetUserAsync(Id);
        var user = new UserViewModel()
        {
            Id = existingUser.Id,
            UserName = existingUser.UserName,
            FirstName = existingUser.FirstName,
            LastName = existingUser.LastName,
            PhoneNumber = existingUser.PhoneNumber,
            Password = existingUser.LoginHint,
            IsActive = existingUser.IsActive,
        };
        return View(user);
    }
}