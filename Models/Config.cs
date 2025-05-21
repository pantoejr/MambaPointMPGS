using MambaPointMPGS.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MambaPointMPGS.Models
{
    public class Config : AuditTrail
    {
        [Key]
        public int Id { get; set; }
        public int ConfigTypeId { get; set; }
        [ForeignKey(nameof(ConfigTypeId))]
        public virtual ConfigType? ConfigType { get; set; }

        [Required]
        [MaxLength(255)]
        public string Value { get; set; } = string.Empty;
    }
}
