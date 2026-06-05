using LendFlow.Systems.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LendFlow.Systems.Models.Entities
{
    public class Disbursement
    {
        public int DisbursementId { get; set; }
        public int ApplicationId { get; set; }
        public int FinanceStaffId { get; set; }
        public decimal DisbursedAmount { get; set; }
        public DateTime DisbursementDate { get; set; }
        public string BankAccountNumber { get; set; } = null!;
        public string BankRoutingCode { get; set; } = null!;
        public string TransactionReferenceNo { get; set; } = null!;
        public DisbursementStatus DisbursementStatus { get; set; } 
    }
}
