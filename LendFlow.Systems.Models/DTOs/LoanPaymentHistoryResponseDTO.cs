namespace LendFlow.Systems.Models.DTOs
{
    public record LoanPaymentHistoryResponseDTO
    {
        public required int ApplicationId { get; init; }
        public required List<PaymentHistoryItemDTO> Payments { get; init; }
    }
}
