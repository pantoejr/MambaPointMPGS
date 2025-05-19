using MambaPointMPGS.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MambaPointMPGS.Repositories;

public class RoleRepository: IRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<RoleRepository> _logger;
    public RoleRepository(RoleManager<IdentityRole> roleManager, ILogger<RoleRepository> logger)
    {
        _roleManager = roleManager;
        _logger = logger;
    }
    public async Task<List<IdentityRole>> GetRoles()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return roles;
    }

    public async Task<bool> AddRole(IdentityRole role)
    {
        try
        {
            await _roleManager.CreateAsync(role);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }
    }

    public async Task<bool> UpdateRole(IdentityRole role)
    {
        try
        {
            await _roleManager.UpdateAsync(role);
            return true;
        }
        catch (Exception e)
        {
           _logger.LogError(e.Message);
           return false;
        }
    }

    public Task<bool> DeleteRole(IdentityRole role)
    {
        throw new NotImplementedException();
    }

    public async Task<IdentityRole> FindRole(string roleId)
    {
        IdentityRole? role = await _roleManager.FindByIdAsync(roleId);
        return role!;
    }
}