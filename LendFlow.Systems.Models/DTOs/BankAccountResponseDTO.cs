namespace LendFlow.Systems.Models.DTOs
{
    public record BankAccountResponseDTO
    {
        public int BankAccountId { get; init; }
        public int UserId { get; init; }
        public string AccountHolderName { get; init; } = null!;
        public string BankAccountNumber { get; init; } = null!;
        public string BankRoutingCode { get; init; } = null!;
        public string BankName { get; init; } = null!;
        public bool IsPrimary { get; init; }
    }
}