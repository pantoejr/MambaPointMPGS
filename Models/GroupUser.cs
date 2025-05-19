using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MambaPointMPGS.Data;

namespace MambaPointMPGS.Models;

public class GroupUser : AuditTrail
{
    [Key]
    public int Id { get; set; }
    public int GroupId { get; set; }
    [ForeignKey(nameof(GroupId))]
    public virtual Group? Group { get; set; }
    
    [Required]
    [MaxLength(255)] 
    public string UserId { get; set; } = string.Empty;
    [ForeignKey(nameof(UserId))]
    public virtual AppUser? User { get; set; }
}