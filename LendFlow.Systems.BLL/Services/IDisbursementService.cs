using LendFlow.Systems.Models.DTOs;
using System.Threading.Tasks;

namespace LendFlow.Systems.BLL.Interfaces
{
    public interface IDisbursementService
    {
        /// <summary>
        /// Validates application approval, processes the payout transaction, 
        /// and builds the complete monthly EMI repayment timeline matrix.
        /// </summary>
        Task<DisbursementResultDTO> ProcessDisbursementAsync(int staffId, int applicationId, DisbursementRequestDTO request);

        Task<IEnumerable<ApprovedLoanQueueDTO>> GetApprovedLoansQueueAsync();
    }
}