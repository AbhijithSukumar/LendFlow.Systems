using LendFlow.Systems.Models.Entities;


namespace LendFlow.Systems.DAL.Interfaces
{
    public interface ICreditRepository
    {
        // Loan Application Commands & Queries
        Task<int> CreateApplicationAsync(LoanApplication application);
        Task<LoanApplication?> GetApplicationByIdAsync(int applicationId);
        Task<IEnumerable<LoanApplication>> GetPendingApplicationsAsync();
        Task<bool> UpdateApplicationStatusAsync(int applicationId, int applicationStatus, decimal approvedLimit, DateTime updatedDate);

        // Credit Evaluation Commands
        Task<bool> LogCreditEvaluationAsync(CreditEvaluation evaluation);
        Task<bool> IsUserKycVerifiedAsync(int userId);

    }
}