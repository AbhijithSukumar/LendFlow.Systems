namespace LendFlow.Systems.Models.DTOs
{
    public record LoanScheduleResponseDTO
    {
        public required int ApplicationId { get; init; }
        public required decimal TotalLoanAmount { get; init; }
        public required int TotalTenureMonths { get; init; }
        public required List<RepaymentScheduleLineDTO> ScheduleLines { get; init; }
    }
}
