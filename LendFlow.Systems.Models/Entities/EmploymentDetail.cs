using LendFlow.Systems.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LendFlow.Systems.Models.Entities
{
    public class EmploymentDetail
    {
        public int UserId { get; set; } // Primary Key & Foreign Key
        public string EmployerName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public decimal AnnualIncome { get; set; }
        public EmploymentType EmploymentType { get; set; }
    }
}
