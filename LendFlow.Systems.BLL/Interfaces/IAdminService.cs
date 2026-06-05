using LendFlow.Systems.Models.DTOs;

namespace LendFlow.Systems.BLL.Interfaces
{
    public interface IAdminService
    {
        Task<int> RegisterAdminAsync(StaffRegisterRequestDTO request);
        Task<string?> LoginAsync(StaffLoginRequestDTO request);
    }
}
