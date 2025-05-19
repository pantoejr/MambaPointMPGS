using System.ComponentModel.DataAnnotations;
using MambaPointMPGS.Data;

namespace MambaPointMPGS.Models;

public class Group : AuditTrail
{
    [Key]
    public int Id { get; set; }

    [Required] [MaxLength(50)] public string Name { get; set; } = string.Empty;
    [MaxLength(255)]
    public string Description { get; set; } = string.Empty;

    public bool IsDeleted { get; set; } = false;
}