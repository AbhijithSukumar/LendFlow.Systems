namespace LendFlow.Systems.Models.DTOs
{
    public record LoanApplicationSummaryDTO
    {
        public int ApplicationId { get; init; }
        public int UserId { get; init; }
        public decimal AmountRequested { get; init; }
        public int TenureMonths { get; init; }
        public string StatusName { get; init; } = null!;
        public DateTime ApplicationDate { get; init; }
    }
}
