using System;
using System.Collections.Generic;
using System.Text;

namespace LendFlow.Systems.Models.DTOs
{
    public record OutstandingBalanceDTO
    {
        public required int ApplicationId { get; init; }
        public required DateTime CalculationAsOf { get; init; }
        public required decimal PrincipalOutstanding { get; init; }
        public required decimal AccruedInterest { get; init; }
        public required decimal OverduePenalties { get; init; }
        public required decimal TotalPayoffAmount { get; init; } // Sum of principal + interest + penalties
        public required int OverdueDaysCount { get; init; }
    }
}
