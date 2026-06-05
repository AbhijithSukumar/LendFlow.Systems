using LendFlow.Systems.Models.DTOs;


namespace LendFlow.Systems.DAL.Interfaces
{
    public interface IDisbursementRepository
    {
       
        Task<(decimal ApprovedLimit, int TenureMonths)?> GetApprovedLoanDetailsAsync(int applicationId);

        Task<DisbursementResultDTO> ExecuteDisbursementAsync(int applicationId, int staffId, decimal amount, DisbursementRequestDTO details);

        Task<bool> SaveRepaymentScheduleAsync(int applicationId, IEnumerable<RepaymentScheduleDTO> scheduleRows);

        Task<IEnumerable<ApprovedLoanQueueDTO>> GetApprovedLoansQueueAsync();
    }
}