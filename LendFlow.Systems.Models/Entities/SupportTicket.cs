using LendFlow.Systems.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LendFlow.Systems.Models.Entities
{
    public class SupportTicket
    {
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public int? AssignedStaffId { get; set; }
        public string Subject { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Priority Priority { get; set; }     
        public TicketStatus TicketStatus { get; set; }   
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
