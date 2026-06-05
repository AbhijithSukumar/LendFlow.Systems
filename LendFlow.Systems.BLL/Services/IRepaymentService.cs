using LendFlow.Systems.Models.DTOs;
using System.Threading.Tasks;

namespace LendFlow.Systems.BLL.Interfaces
{
    public interface IRepaymentService
    {
        /// <summary>
        /// Validates payment metadata thresholds and submits the transaction to the ledger engine.
        /// </summary>
        Task<bool> ProcessRepaymentAsync(ProcessRepaymentDTO repayment);

        Task<BorrowerLoanSummaryDTO?> GetBorrowerSummaryAsync(int userId);
        Task<IEnumerable<LoanScheduleGridItemDTO>> GetScheduleGridAsync(int applicationId);
        Task<IEnumerable<AdminActiveLoanOverviewDTO>> GetAdminActiveLoansAsync();
    }
}