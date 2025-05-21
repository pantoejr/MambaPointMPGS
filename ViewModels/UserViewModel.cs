using System.ComponentModel.DataAnnotations;

namespace MambaPointMPGS.ViewModels;

public class UserViewModel
{
    public string? Id { get;set; }
    [Required]
    [DataType(DataType.Text)]
    public string FirstName { get; set; } = string.Empty;
    [Required] 
    [DataType(DataType.Text)]
    public string LastName { get; set; } = string.Empty;
    public string? UserName { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string PasswordConfirm { get; set; } = string.Empty;
    public int GroupId { get; set; }
    public bool IsActive { get; set; }
}