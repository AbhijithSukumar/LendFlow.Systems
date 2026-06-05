using LendFlow.Systems.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LendFlow.Systems.Models.Entities
{
    public class RepaymentSchedule
    {
        public int ScheduleId { get; set; }
        public int ApplicationId { get; set; }
        public int InstallmentNumber { get; set; }
        public DateTime DueDate { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal TotalInstallmentAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; } 
    }
}
