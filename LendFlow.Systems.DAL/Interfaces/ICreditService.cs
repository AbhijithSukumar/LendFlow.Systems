using LendFlow.Systems.Models.DTOs;


namespace LendFlow.Systems.BLL.Interfaces
{
    public interface ICreditService
    {
        // For Borrowers submitting applications
        Task<int> SubmitLoanApplicationAsync(int userId, LoanApplicationRequestDTO request);

        // For Credit Underwriters retrieving pending work
        Task<IEnumerable<LoanApplicationSummaryDTO>> GetPendingApplicationsAsync();

        // For Credit Underwriters processing evaluations
        Task<bool> ProcessLoanEvaluationAsync(int underwriterStaffId, int applicationId, CreditEvaluationRequestDTO request);
    }
}