namespace LendFlow.Systems.Models.DTOs
{
    public record AddBankAccountDTO
    {
        public required string AccountHolderName { get; init; }
        public required string BankAccountNumber { get; init; }
        public required string BankRoutingCode { get; init; }
        public required string BankName { get; init; }
        public bool IsPrimary { get; init; }
    }
}
