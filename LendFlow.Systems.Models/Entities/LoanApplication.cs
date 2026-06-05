using LendFlow.Systems.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LendFlow.Systems.Models.Entities
{
    public class LoanApplication
    {
        public int ApplicationId { get; set; }
        public int UserId { get; set; }
        public decimal AmountRequested { get; set; }
        public int TenureMonths { get; set; }
        public DateTime ApplicationDate { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; } 
    }
}
