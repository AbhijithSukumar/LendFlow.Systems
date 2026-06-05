using LendFlow.Systems.Models.DTOs;


namespace LendFlow.Systems.BLL.Interfaces
{
    public interface IKycService
    {
        Task<bool> ProcessKYCVerificationAsync(int adminId, KYCUpdateRequestDTO request);

        Task<string?> CheckVerificationStatus(int usedId);
    }
}
