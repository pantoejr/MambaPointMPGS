using System.ComponentModel.DataAnnotations;

namespace MambaPointMPGS.ViewModels;

public class LoginViewModel
{
    [Required] 
    public string UserName { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    public bool RememberMe { get; set; } = false;
}