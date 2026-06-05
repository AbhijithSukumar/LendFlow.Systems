namespace LendFlow.Systems.Models.DTOs
{
    public record BorrowerLoanSummaryDTO
    {
        public int ApplicationId { get; init; }
        public decimal TotalLoanAmount { get; init; }
        public decimal TotalPaidToDate { get; init; }
        public decimal NextEmiAmount { get; init; }
        public DateTime? NextEmiDueDate { get; init; }
    }
}
