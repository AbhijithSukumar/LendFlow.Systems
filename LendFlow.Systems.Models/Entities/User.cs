using System;
using System.Collections.Generic;
using System.Text;

namespace LendFlow.Systems.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;     
        public PersonalDetail? PersonalInfo { get; set; }
        public EmploymentDetail? EmploymentInfo { get; set; }
        public KYCDetail? KYCInfo { get; set; }

    }
}
