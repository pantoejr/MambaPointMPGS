using MambaPointMPGS.Services;
using MambaPointMPGS.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MambaPointMPGS.Controllers;

public class RoleController : Controller
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<IActionResult> Index()
    {
        var roles = await _roleService.GetRoles();
        return View(roles);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RoleViewModel model)
    {
        if (String.IsNullOrEmpty(model.Name))
        {
            TempData["message"] = "Name is required";
            TempData["flag"] = "warning";
            return View(model);
        }

        var newRole = new IdentityRole()
        {
            Name = model.Name,
        };
        var result = await _roleService.AddRole(newRole);
        if (result)
        {
            TempData["message"] = "Role added successfully";
            TempData["flag"] = "success";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["message"] = "Role could not be added";
            TempData["flag"] = "danger";
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string Id)
    {
        var role = await _roleService.FindRole(Id);
        return View(role);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string Id, IdentityRole role)
    {
        if (String.IsNullOrEmpty(Id))
        {
            TempData["message"] = "Error updating role";
            TempData["flag"] = "danger";
            return View(role);
        }
        var result = await _roleService.UpdateRole(role);
        if (!result)
        {
            TempData["message"] = "Error updating role";
            TempData["flag"] = "danger";
            return View(role);
        }
        TempData["message"] = "Role updated successfully";
        TempData["flag"] = "success";
        return RedirectToAction(nameof(Index));
    }
}