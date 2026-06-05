using System;
using System.Collections.Generic;
using System.Text;

namespace LendFlow.Systems.Models.DTOs
{
    public record TicketResolveDTO
      (
          int NewStatus, // 3 = Resolved, 4 = Closed
          string InternalNotes
      );
}
