using MambaPointMPGS.Data;
using System.ComponentModel.DataAnnotations;

namespace MambaPointMPGS.Models
{
    public class ConfigType : AuditTrail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)] public string Name { get; set; } = string.Empty;
    }
}
