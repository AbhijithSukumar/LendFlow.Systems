namespace LendFlow.Systems.Models.DTOs
{
    public record ApprovedLoanQueueDTO
    {
        public int ApplicationId { get; init; }
        public int UserId { get; init; }
        public decimal ApprovedLimit { get; init; }
        public int TenureMonths { get; init; }
        public DateTime ApplicationDate { get; init; }
    }
}