namespace MambaPointMPGS.Data;

public class AuditTrail
{
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
    public DateTime ModifiedOn { get; set; }
    public string DeletedBy { get; set; } = string.Empty;
    public DateTime DeletedOn { get; set; }
}