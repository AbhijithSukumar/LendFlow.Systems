namespace LendFlow.Systems.Models.Entities
{
    public class ComplianceAuditLog
    {
        public long AuditLogId { get; set; }
        public int StaffId { get; set; }
        public string ActionType { get; set; } = null!;
        public string AffectedTableName { get; set; } = null!;
        public int RecordId { get; set; }
        public string? OldValues { get; set; } // JSON String
        public string? NewValues { get; set; } // JSON String     
        public string? IpAddress { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
