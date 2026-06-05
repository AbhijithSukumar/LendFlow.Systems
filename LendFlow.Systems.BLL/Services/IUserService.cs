using LendFlow.Systems.Models.DTOs;

namespace LendFlow.Systems.BLL.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterFullUserProfileAsync(RegistrationRequestDTO request);

        Task<string?> LoginUserAsync(LoginRequestDTO request);
    }
}
