using LendFlow.Systems.Models.Entities;

namespace LendFlow.Systems.DAL.Interfaces
{
    public interface IStaffRepository
    {
        Task<int> RegisterStaffAsync(InternalStaff staff);
        Task<InternalStaff?> GetStaffByEmailAsync(string email);
    }
}
