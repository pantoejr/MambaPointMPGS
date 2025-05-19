using MambaPointMPGS.Data;
using MambaPointMPGS.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace MambaPointMPGS.Services;

public interface IUserService
{
    Task<List<AppUser>> GetAllAsync();
    Task<bool> AddUserAsync(UserViewModel user);
    Task<bool> UpdateUserAsync(AppUser user);
    Task<bool> DeleteUserAsync(AppUser user);
    Task<AppUser> GetUserAsync(string userId);
}