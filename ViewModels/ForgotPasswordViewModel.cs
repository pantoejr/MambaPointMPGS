using System.ComponentModel.DataAnnotations;

namespace MambaPointMPGS.ViewModels;

public class ForgotPasswordViewModel
{
    [Required]
    public string Email { get; set; } = string.Empty;
}