using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MambaPointMPGS.Data;

public class AppUser : IdentityUser
{
    [Required] [MaxLength(100)] public string FirstName { get; set; } = string.Empty;
    [Required] [MaxLength(100)] public string LastName { get; set; } = string.Empty;
    [MaxLength(255)] public string? LoginHint { get; set; }
    public bool IsActive { get; set; } = true;
}