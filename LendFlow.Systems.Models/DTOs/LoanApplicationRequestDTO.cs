namespace LendFlow.Systems.Models.DTOs
{
    public record LoanApplicationRequestDTO
    {
        public required decimal AmountRequested { get; init; }
        public required int TenureMonths { get; init; }
    }
}
