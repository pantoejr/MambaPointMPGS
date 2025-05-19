using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MambaPointMPGS.Data;
using Microsoft.AspNetCore.Identity;

namespace MambaPointMPGS.Models;

public class GroupRole : AuditTrail
{
    [Key]
    public int Id { get; set; }
    public int GroupId { get; set; }
    [ForeignKey(nameof(GroupId))]
    public virtual Group? Group { get; set; }
    
    [MaxLength(255)]
    public string RoleId { get; set; } = string.Empty;
    [ForeignKey(nameof(RoleId))]
    public virtual IdentityRole? Role { get; set; }
    
}