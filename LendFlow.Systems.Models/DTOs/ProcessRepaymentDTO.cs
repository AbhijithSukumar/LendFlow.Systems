namespace LendFlow.Systems.Models.DTOs
{
    public record ProcessRepaymentDTO
    {
        public required int ApplicationId { get; init; }
        public required decimal PaymentAmount { get; init; }
        public required string PaymentMethod { get; init; } // e.g., "UPI", "BankTransfer", "Card"
        public required string TransactionReferenceNo { get; init; } // External UTR/Receipt number
    }
}