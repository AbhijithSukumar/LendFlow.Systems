namespace LendFlow.Systems.Models.DTOs
{
    public record CreditEvaluationRequestDTO
    {
        public required int CreditScore { get; init; }
        public required decimal DebtToIncomeRatio { get; init; }
        public required decimal ApprovalLimit { get; init; }
        public required string UnderwriterNotes { get; init; }

        public required int NewStatus { get; init; } // 3 = Approved, 4 = Rejected
    }
}
