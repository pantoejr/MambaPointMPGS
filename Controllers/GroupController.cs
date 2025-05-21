using MambaPointMPGS.Data;
using MambaPointMPGS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MambaPointMPGS.Controllers;

public class GroupController : Controller
{
    private readonly AppDbContext _context;
    public GroupController(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var groups = await _context.Groups.ToListAsync();
        return View(groups);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Group model)
    {
        if (String.IsNullOrEmpty(model.Name))
        {
            TempData["message"] = "Error creating group";
            TempData["flag"] = "danger";
            return View(model);
        }
        await _context.Groups.AddAsync(model);
        await _context.SaveChangesAsync();
        TempData["message"] = "Group created successfully";
        TempData["flag"] = "success";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int Id)
    {
        var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == Id);
        return View(group);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int Id, Group model)
    {
        try
        {
            _context.Groups.Update(model);
            await _context.SaveChangesAsync();
            TempData["message"] = "Group updated successfully";
            TempData["flag"] = "success";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["message"] = ex.Message;
            TempData["flag"] = "danger";
            return View(model);
        }
       
    }

    [HttpGet]
    public IActionResult Details(int Id)
    {
        var group = _context.Groups.FirstOrDefault(x => x.Id.Equals(Id));
        return View(group);
    }


    [HttpGet]
    public async Task<IActionResult> UnAvGroupRoles(int Id)
    {
        var unAvailableGroupRoles = await _context.Roles.Where(role => _context.GroupRoles
            .Where(gr => gr.GroupId == Id)
            .Select(gr => gr.RoleId)
            .Contains(role.Id))
            .ToListAsync();

        ViewData["GroupID"] = Id;
        return PartialView("_UnAvGroupRoles", unAvailableGroupRoles);
    }

    [HttpGet]
    public async Task<IActionResult> AvGroupRoles(int Id)
    {
        var availableGroupRoles = await _context.Roles.Where(role => !_context.GroupRoles
            .Where(gr => gr.GroupId == Id)
            .Select(gr => gr.RoleId)
            .Contains(role.Id))
            .ToListAsync();

        ViewData["GroupID"] = Id;

        return PartialView("_AvGroupRoles", availableGroupRoles);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddGroupRole(int groupID, string roleID)
    {
        try
        {
            var newGroupRole = new GroupRole
            {
                RoleId = roleID,
                GroupId = groupID,
                CreatedBy = "system",
                CreatedOn = DateTime.Now,
                ModifiedBy = "system",
                ModifiedOn = DateTime.Now,
            };
            await _context.GroupRoles.AddAsync(newGroupRole);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            TempData["message"] = "Error: " + ex.Message;
            TempData["flag"] = "danger";
            return RedirectToAction(nameof(Details), new { Id = groupID });
        }
        TempData["message"] = "Role added to group successfully";
        TempData["flag"] = "success";
        return RedirectToAction(nameof(Details), new { Id = groupID });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveGroupRole(int groupID, string roleID)
    {
        try
        {
            var existingGroupRole = await _context.GroupRoles.FirstOrDefaultAsync(x => x.GroupId.Equals(groupID) && x.RoleId.Equals(roleID));
            _context.GroupRoles.Remove(existingGroupRole);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            TempData["message"] = "Error: " + ex.Message;
            TempData["flag"] = "danger";
            return RedirectToAction(nameof(Details), new { Id = groupID });
        }
        TempData["message"] = "Role removed from group successfully";
        TempData["flag"] = "success";
        return RedirectToAction(nameof(Details), new { Id = groupID });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int Id)
    {
        try
        {
            var group = await _context.Groups.FirstOrDefaultAsync(x => x.Id == Id);
            _context.Groups.Remove(group!);
            await _context.SaveChangesAsync();
            TempData["message"] = "Group deleted successfully";
            TempData["flag"] = "success";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {

            TempData["message"] = "Error: " + ex.Message;
            TempData["flag"] = "danger";
            return RedirectToAction(nameof(Index));
        }
       
    }
}