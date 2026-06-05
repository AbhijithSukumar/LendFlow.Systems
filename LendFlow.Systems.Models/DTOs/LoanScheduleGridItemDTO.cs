namespace LendFlow.Systems.Models.DTOs
{
    public record LoanScheduleGridItemDTO
    {
        public int InstallmentNumber { get; init; }
        public DateTime DueDate { get; init; }
        public decimal TotalInstallmentAmount { get; init; }
        public decimal AmountPaid { get; init; }
        public string Status { get; init; } = "Pending"; // e.g., "Pending", "Paid"
        public DateTime? PaymentDate { get; init; }
    }
}
