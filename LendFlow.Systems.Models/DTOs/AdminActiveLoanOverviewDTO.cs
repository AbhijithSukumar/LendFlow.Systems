namespace LendFlow.Systems.Models.DTOs
{
    public record AdminActiveLoanOverviewDTO
    {
        public int ApplicationId { get; init; }
        public int UserId { get; init; }
        public string BorrowerName { get; init; } = string.Empty;
        public decimal ApprovedLimit { get; init; }
        public int TenureMonths { get; init; }
        public decimal TotalCollected { get; init; }
        public string LoanStatus { get; init; } = "Active";
    }
}
