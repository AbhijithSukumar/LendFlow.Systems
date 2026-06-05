namespace LendFlow.Systems.DAL.Interfaces
{
    public interface IKycRepository
    {
        Task<bool> UpdateKYCWithAuditAsync(int userId, int status, int adminId, string notes);

        Task<string?> GetKycStatusByUserIdAsync(int userId);
    }
}
