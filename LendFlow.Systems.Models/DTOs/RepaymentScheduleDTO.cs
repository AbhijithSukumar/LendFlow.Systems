namespace LendFlow.Systems.Models.DTOs
{
    public record RepaymentScheduleDTO
    {
        public int InstallmentNumber { get; init; }
        public DateTime DueDate { get; init; }
        public decimal TotalInstallmentAmount { get; init; }
        public string Status { get; init; } = null!;
    }
}
