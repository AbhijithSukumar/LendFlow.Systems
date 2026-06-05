namespace LendFlow.Systems.Models.DTOs
{
    public record AuditLogSummaryDTO
    {
        public long AuditLogId { get; init; }
        public int StaffId { get; init; }
        public string ActionType { get; init; } = null!;
        public DateTime Timestamp { get; init; }
    }
}
