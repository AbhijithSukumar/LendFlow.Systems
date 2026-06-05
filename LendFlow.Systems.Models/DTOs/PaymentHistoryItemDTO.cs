namespace LendFlow.Systems.Models.DTOs
{
    public record PaymentHistoryItemDTO
    {
        public required string TransactionReferenceNo { get; init; } // UTR / Gateway ID
        public required DateTime TransactionDate { get; init; }
        public required decimal AmountPaid { get; init; }
        public required string PaymentMethod { get; init; } // e.g., "UPI", "BankTransfer"
        public required string ProcessingStatus { get; init; } // e.g., "Success", "Settled", "Reversed"
    }
}
