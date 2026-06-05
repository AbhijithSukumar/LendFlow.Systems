namespace LendFlow.Systems.Models.DTOs
{
    public record RepaymentScheduleLineDTO
    {
        public required int InstallmentNo { get; init; }
        public required DateTime DueDate { get; init; }
        public required decimal PrincipalComponent { get; init; }
        public required decimal InterestComponent { get; init; }
        public required decimal TotalEmiAmount { get; init; }
        public required decimal RemainingPrincipalBalance { get; init; }
        public required string Status { get; init; } // e.g., "Pending", "Paid", "PartiallyPaid", "Overdue"
        public DateTime? PaidDate { get; init; }
    }
}
