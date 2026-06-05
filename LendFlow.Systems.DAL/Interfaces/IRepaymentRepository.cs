using LendFlow.Systems.Models.DTOs;
using LendFlow.Systems.Models.Entities;
using System.Threading.Tasks;

namespace LendFlow.Systems.DAL.Interfaces
{
    public interface IRepaymentRepository
    {
        /// <summary>
        /// Registers a financial payment in the ledger database and triggers the allocation pipeline.
        /// </summary>
        Task<bool> SubmitRepaymentAsync(ProcessRepaymentDTO paymentDetails);

        Task<BorrowerLoanSummaryDTO?> GetBorrowerSummaryAsync(int userId);
        Task<IEnumerable<LoanScheduleGridItemDTO>> GetScheduleGridAsync(int applicationId);
        Task<IEnumerable<AdminActiveLoanOverviewDTO>> GetAdminActiveLoansAsync();
    }
}