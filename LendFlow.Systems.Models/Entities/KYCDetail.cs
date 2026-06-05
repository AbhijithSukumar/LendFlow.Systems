using LendFlow.Systems.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LendFlow.Systems.Models.Entities
{
    public class KYCDetail
    {
        public int UserId { get; set; } // Primary Key & Foreign Key
        public DocumentType DocumentType { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public VerificationStatus VerificationStatus { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
