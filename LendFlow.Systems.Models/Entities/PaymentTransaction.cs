namespace LendFlow.Systems.Models.Entities
{
    public class PaymentTransaction
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string TransactionReferenceNo { get; set; } = string.Empty; // UTR or Gateway Reference
        public decimal AmountPaid { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PaymentMethod { get; set; } = string.Empty; // UPI, NetBanking, DebitCard
        public string ProcessingStatus { get; set; } = "Success"; // Success, Failed, Reversed
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
