using System.ComponentModel.DataAnnotations;

namespace MambaPointMPGS.ViewModels;

public class RoleViewModel
{
    [Required]
    [DataType(DataType.Text)]
    public string Name { get; set; } = string.Empty;
}