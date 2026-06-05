namespace LendFlow.Systems.Models.Entities
{
    public class LoanAccountSummary
    {
        public int ApplicationId { get; set; }
        public int BorrowerUserId { get; set; }
        public decimal ApprovedLimit { get; set; }
        public int TenureMonths { get; set; }
        public decimal RemainingPrincipal { get; set; }   // Current unpaid principal balance
        public decimal AccruedInterestBalance { get; set; } // Current unpaid interest outstanding
        public string LoanStatus { get; set; } = "Active"; // Active, Delinquent, Closed
        public DateTime DisbursedAt { get; set; }
        public DateTime LastPaymentDate { get; set; }
    }
}
