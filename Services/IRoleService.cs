using Microsoft.AspNetCore.Identity;

namespace MambaPointMPGS.Services;

public interface IRoleService
{
    Task<List<IdentityRole>> GetRoles();
    Task<bool> AddRole(IdentityRole role);
    Task<bool> UpdateRole(IdentityRole role);
    Task<bool> DeleteRole(IdentityRole role);
    Task<IdentityRole> FindRole(string roleId);
}