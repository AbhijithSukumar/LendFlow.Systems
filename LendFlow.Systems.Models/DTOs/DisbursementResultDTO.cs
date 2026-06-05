namespace LendFlow.Systems.Models.DTOs
{
    public record DisbursementResultDTO
    {
        public bool IsSuccess { get; init; }
        public string? TransactionId { get; init; }
        public string? ErrorMessage { get; init; }
    }
}
