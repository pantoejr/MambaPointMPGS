using MambaPointMPGS.Data;
using MambaPointMPGS.Models;
using MambaPointMPGS.Services;
using MambaPointMPGS.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MambaPointMPGS.Repositories;

public class UserRepository: IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<UserRepository> _logger;
    private readonly AppDbContext _context;
    public UserRepository(UserManager<AppUser> userManager, ILogger<UserRepository> logger, AppDbContext context)
    {
        _userManager = userManager;
        _logger = logger;
        _context = context;
    }
    
    public async Task<List<AppUser>> GetAllAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        return users;
    }

    public async Task<bool> AddUserAsync(UserViewModel user)
    { 
        try
        {
            var newUser = new AppUser()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.Email,
                NormalizedEmail = user.Email.ToUpper(),
                NormalizedUserName = user.Email.ToUpper(),
                LoginHint = user.PasswordConfirm,
                IsActive = true,
            };
            var result = await _userManager.CreateAsync(newUser, user.PasswordConfirm);
            List<string?> groupRoles = await _context.GroupRoles.Where(x=>x.GroupId == user.GroupId).Select(x=>x.Role!.Name).ToListAsync();

            if (result.Succeeded)
            {
                var userGroup = new GroupUser
                {
                    GroupId = user.GroupId,
                    UserId = newUser.Id,
                };
                await _context.GroupUsers.AddAsync(userGroup);
                await _userManager.AddToRolesAsync(newUser, groupRoles);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }
    }

    public async Task<bool> UpdateUserAsync(AppUser user)
    {
        try
        {
            await _userManager.UpdateAsync(user);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }
    }

    public async Task<bool> DeleteUserAsync(AppUser user)
    {
        try
        {
            await _userManager.DeleteAsync(user);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }
    }

    public async Task<AppUser> GetUserAsync(string userId)
    {
        AppUser? user = await _userManager.FindByIdAsync(userId);
        return user!;
    }
}