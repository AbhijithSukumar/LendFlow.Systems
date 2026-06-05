namespace LendFlow.Systems.Models.Entities
{
    public class CreditEvaluation
    {
        public int EvaluationId { get; set; }
        public int ApplicationId { get; set; }
        public int UnderwriterStaffId { get; set; }
        public int CreditScore { get; set; }
        public decimal DebtToIncomeRatio { get; set; }
        public decimal ApprovalLimit { get; set; }
        public string? UnderwriterNotes { get; set; }
        public DateTime EvaluationDate { get; set; }
    }
}
